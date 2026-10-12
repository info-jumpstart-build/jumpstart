using System.Net;
using System.Net.Sockets;
using jumptest;
using jumptest.core;

// Configuration for dynamic port binding
const int StartPort = 5000;
const int EndPort = 5100;
const int MaxRetries = 100;

<<<<<<< HEAD
int boundPort = 0;
//IPAddress boundIP = Config.getNetworkInterfaceIP();
IPAddress boundIP = IPAddress.Any; // Listen on all interfaces. The actual IP will be determined by the client when it connects.
var builder = WebApplication.CreateBuilder(args);

// Try to find an available port
for (int port = StartPort; port <= EndPort && port < StartPort + MaxRetries; port++)
{
    try
    {
        // Configure Kestrel to listen on the resolved network interface
        builder.WebHost.UseUrls($"http://{boundIP}:{port}");

        // Test if port is available by trying to bind
        using (var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
        {
            socket.Bind(new IPEndPoint(boundIP, port));
        }

        boundPort = port;
        Console.WriteLine($"Scheduler will bind to IP: {boundIP}, Port: {boundPort}");
        break;
    }
    catch (SocketException)
    {
        // Port is in use, try next port
        continue;
=======
//IPAddress boundIP = Config.getNetworkInterfaceIP();
IPAddress boundIP = IPAddress.Any; // Listen on all interfaces. The actual IP will be determined by the client when it connects.

int boundPort = 0;

// A fixed port from servers.scheduler.port in ~/.<namespace>.json (if
// configured) is tried first, exactly -- not silently skipped if it's busy. A
// busy configured port is usually a stale process from a previous run;
// scanning forward from it (with a warning) instead of silently landing
// elsewhere keeps that mismatch visible in the log rather than surfacing
// later as misrouted dispatch (a stale process holding the port other nodes
// expect, while a freshly started one is on a different port).
int? configuredPort = Config.GetServerPort("scheduler");
if (configuredPort.HasValue)
{
    boundPort = TryBindPort(configuredPort.Value, boundIP);
    if (boundPort == 0)
    {
        Console.WriteLine($"WARNING: configured port {configuredPort.Value} (servers.scheduler.port in ~/.<namespace>.json) is already in use -- possibly a stale process from a previous run. Scanning forward for a free port instead; kill the stale process if clients expect {configuredPort.Value} specifically.");
        boundPort = FindFreePort(configuredPort.Value, configuredPort.Value + (EndPort - StartPort), MaxRetries, boundIP);
>>>>>>> main
    }
}

if (boundPort == 0)
{
<<<<<<< HEAD
=======
    boundPort = FindFreePort(StartPort, EndPort, MaxRetries, boundIP);
}

if (boundPort == 0)
{
>>>>>>> main
    Console.WriteLine($"ERROR: Could not find available port in range {StartPort}-{EndPort}");
    Environment.Exit(1);
}

<<<<<<< HEAD
=======
var builder = WebApplication.CreateBuilder(args);
// Configure Kestrel to listen on the resolved network interface
builder.WebHost.UseUrls($"http://{boundIP}:{boundPort}");
Console.WriteLine($"Scheduler will bind to IP: {boundIP}, Port: {boundPort}");

static int TryBindPort(int port, IPAddress ip)
{
    try
    {
        using var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        socket.Bind(new IPEndPoint(ip, port));
        return port;
    }
    catch (SocketException)
    {
        return 0;
    }
}

static int FindFreePort(int start, int end, int maxRetries, IPAddress ip)
{
    for (int port = start; port <= end && port < start + maxRetries; port++)
    {
        if (TryBindPort(port, ip) != 0) return port;
    }
    return 0;
}

>>>>>>> main
// ── Auth0 M2M token provider (outbound calls to ScriptAgents) ───────────────
M2MTokenProvider.Initialize(builder.Configuration);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ── Auth0 JWT Bearer validation (inbound requests from API server) ───────────
builder.Services.AddAuthentication(Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = $"https://{builder.Configuration["Auth0:Domain"]}/";
        options.Audience  =  builder.Configuration["Auth0:Audience"];
    });
builder.Services.AddAuthorization();

builder.Services.AddCors(options =>
{
    options.AddPolicy("Access-Control-Allow-Origin",
                    builder =>
                    {
                        builder.WithOrigins("*")
                           .AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
                    });

});

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

// Register notification callback
NotificationRegistrar.Register();

// Register scheduler after successful binding
var registrationTask = Task.Run(async () =>
{
    try
    {
        // Wait a moment for the server to fully start
        await Task.Delay(1000);
        
        // Call registration callback
        await RegisterScheduler(boundIP, boundPort);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error during scheduler registration: {ex.Message}");
    }
});

// Start dispatcher thread
var dispatcherThread = new jumptest.DispatcherThread();
dispatcherThread.Start();

// Start Quartz-based scheduler thread — reads workflow schedules from the database
// and fires SchedulerLogic.execute() when a cron trigger is due.
var quartzSchedulerThread = new jumptest.core.QuartzSchedulerThread();
quartzSchedulerThread.Start();

// Start health monitor thread
var healthMonitorThread = new jumptest.HealthMonitorThread(TimeSpan.FromSeconds(30));
healthMonitorThread.Start();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("Access-Control-Allow-Origin");

//app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

// Graceful shutdown - complete pending jobs
app.Lifetime.ApplicationStopping.Register(() =>
{
    Console.WriteLine("Application stopping - completing pending jobs...");
    
    // Stop Quartz scheduler thread
    quartzSchedulerThread.Stop();

    // Stop health monitor
    healthMonitorThread.Stop();
    
    // Stop accepting new jobs
    jumptest.DispatcherThread.Queue.CompleteAdding();
    
    // Wait for dispatcher thread to finish (with timeout)
    if (!dispatcherThread.Join(TimeSpan.FromSeconds(30)))
    {
        Console.WriteLine("Warning: Dispatcher thread did not complete within timeout");
    }
    
    // Wait for health monitor thread to finish (with timeout)
    if (!healthMonitorThread.Join(TimeSpan.FromSeconds(5)))
    {
        Console.WriteLine("Warning: Health monitor thread did not complete within timeout");
    }
    ServerNode serverNode = jumptest.core.SchedulerLogic.ThisServerNode;
    if (serverNode != null && serverNode.id > 0)
    {
        jumptest.core.SchedulerLogic.Create().unregister(serverNode.id);
    }
});

app.Run();

// Registration callback function
static async Task RegisterScheduler(IPAddress boundIP, int port)
{
    // Get SessionInfo singleton and populate it with system information
    var sessionInfo = SessionInfo.Instance;
    Util.PopulateSessionInfo(sessionInfo);
    var computerName = sessionInfo.GetValue("ComputerName", Environment.MachineName);

    // Add scheduler-specific information to the session
    sessionInfo.SetValue("SchedulerIPAddress", boundIP.ToString());
    sessionInfo.SetValue("SchedulerPort", port.ToString());
    sessionInfo.SetValue("SchedulerURL", $"http://{computerName}:{port}");
    
    // Set ServerNode keys that are used in registration
    sessionInfo.SetValue("ServerNodeIPAddress", boundIP.ToString());
    sessionInfo.SetValue("ServerNodeURL", $"http://{computerName}:{port}");
    

    var schedulerId = $"{computerName}:{port}";
    
    Console.WriteLine($"==============================================");
    Console.WriteLine($"Scheduler Registration");
    Console.WriteLine($"==============================================");
    Console.WriteLine($"Scheduler ID: {schedulerId}");
    Console.WriteLine($"Computer Name: {computerName}");
    Console.WriteLine($"IP Address: {boundIP}");
    Console.WriteLine($"Port: {port}");
    Console.WriteLine($"URL: {sessionInfo.GetValue("ServerNodeURL")}");
    Console.WriteLine($"User: {sessionInfo.GetValue("UserName", "Unknown")}");
    Console.WriteLine($"Domain: {sessionInfo.GetValue("UserDomain", "Unknown")}");
    Console.WriteLine($"==============================================");
    

    
    
    var registrationData = new ServerNode        {
            
            hostname = computerName,
            server_node_type_id = (long)ServerNode.ServerNodeType.Scheduler,
            ip_address = sessionInfo.GetValue("ServerNodeIPAddress"),
            port = port,
            url = sessionInfo.GetValue("ServerNodeURL"),
            username = sessionInfo.GetValue("UserName"),
            user_domain = sessionInfo.GetValue("UserDomain"),
            os_name = sessionInfo.GetValue("OSName"),
            os_version = sessionInfo.GetValue("OSVersion"),
            architecture = sessionInfo.GetValue("Architecture"),
            registered_at = DateTime.UtcNow,
            server_node_status_id = (long)ServerNode.ServerNodeStatus.Online,
            is_active = 1,
            created_by = Environment.UserName,
            
        };

        SchedulerLogic.Create().register(registrationData);
        
    
}
