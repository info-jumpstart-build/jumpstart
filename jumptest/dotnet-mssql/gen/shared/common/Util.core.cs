using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Principal;
using System.Net;
using System.Runtime.InteropServices;

namespace jumptest
{
    public class Util
    {
        /// <summary>
        /// Populates the singleton SessionInfo instance with current computer name and user information.
        /// </summary>
        public static void PopulateSessionInfo()
        {
            PopulateSessionInfo(SessionInfo.Instance);
        }

        /// <summary>
        /// Populates a SessionInfo object with current computer name and user information.
        /// </summary>
        /// <param name="sessionInfo">The SessionInfo object to populate with current system information.</param>
        public static void PopulateSessionInfo(SessionInfo sessionInfo)
        {
            if (sessionInfo == null)
            {
                throw new ArgumentNullException(nameof(sessionInfo));
            }

            try
            {
                // Get current computer name
                string computerName = Environment.MachineName;
                sessionInfo.SetValue("ComputerName", computerName);

                // Get current user name
                string userName = Environment.UserName;
                sessionInfo.SetValue("UserName", userName);

                // Get current domain (if available)
                string userDomain = Environment.UserDomainName;
                sessionInfo.SetValue("UserDomain", userDomain);

                // Get fully qualified domain name (if available)
                string fqdn = Dns.GetHostName();
                try
                {
                    var hostEntry = Dns.GetHostEntry(fqdn);
                    sessionInfo.SetValue("FQDN", hostEntry.HostName);
                }
                catch
                {
                    // If DNS resolution fails, just use the hostname
                    sessionInfo.SetValue("FQDN", fqdn);
                }

                // Get Windows identity information (if available)
                try
                {
                    WindowsIdentity identity = WindowsIdentity.GetCurrent();
                    if (identity != null)
                    {
                        sessionInfo.SetValue("WindowsIdentity", identity.Name);
                        sessionInfo.SetValue("AuthenticationType", identity.AuthenticationType ?? "Unknown");
                        sessionInfo.SetValue("IsAuthenticated", identity.IsAuthenticated.ToString());
                    }
                }
                catch
                {
                    // Windows identity not available (e.g., on non-Windows platforms)
                    sessionInfo.SetValue("WindowsIdentity", "Not Available");
                    sessionInfo.SetValue("AuthenticationType", "Not Available");
                    sessionInfo.SetValue("IsAuthenticated", "false");
                }

                // Get current working directory
                string currentDirectory = Environment.CurrentDirectory;
                sessionInfo.SetValue("CurrentDirectory", currentDirectory);

                // Get operating system information
                sessionInfo.SetValue("OSVersion", Environment.OSVersion.ToString());
                sessionInfo.SetValue("Platform", Environment.OSVersion.Platform.ToString());
                
                // Get detailed operating system name
                string osName = GetOperatingSystemName();
                sessionInfo.SetValue("OSName", osName);
                
                // Get processor architecture
                string architecture = GetProcessorArchitecture();
                sessionInfo.SetValue("Architecture", architecture);
                
                // Get combined OS and architecture info
                sessionInfo.SetValue("OSAndArchitecture", $"{osName} ({architecture})");
            }
            catch (Exception ex)
            {
                // Log the exception but don't throw - populate what we can
                sessionInfo.SetValue("Error", $"Failed to populate some session info: {ex.Message}");
            }
        }

        /// <summary>
        /// Gets the operating system name (Windows, macOS, Linux, etc.)
        /// </summary>
        /// <returns>The operating system name</returns>
        private static string GetOperatingSystemName()
        {
            try
            {
                var osPlatform = Environment.OSVersion.Platform;
                
                if (osPlatform == PlatformID.Win32NT)
                {
                    return "Windows";
                }
                else if (osPlatform == PlatformID.Unix)
                {
                    // Check if it's macOS or Linux
                    try
                    {
                        var process = new System.Diagnostics.Process
                        {
                            StartInfo = new System.Diagnostics.ProcessStartInfo
                            {
                                FileName = "uname",
                                Arguments = "-s",
                                UseShellExecute = false,
                                RedirectStandardOutput = true,
                                CreateNoWindow = true
                            }
                        };
                        
                        process.Start();
                        string output = process.StandardOutput.ReadToEnd().Trim();
                        process.WaitForExit();
                        
                        if (output.Equals("Darwin", StringComparison.OrdinalIgnoreCase))
                        {
                            return "macOS";
                        }
                        else if (output.Equals("Linux", StringComparison.OrdinalIgnoreCase))
                        {
                            return "Linux";
                        }
                        else
                        {
                            return $"Unix ({output})";
                        }
                    }
                    catch
                    {
                        // Fallback: try to detect macOS by checking for common macOS paths
                        if (System.IO.Directory.Exists("/Applications") && System.IO.Directory.Exists("/System"))
                        {
                            return "macOS";
                        }
                        return "Linux";
                    }
                }
                else if (osPlatform == PlatformID.MacOSX)
                {
                    return "macOS";
                }
                else
                {
                    return osPlatform.ToString();
                }
            }
            catch
            {
                return "Unknown";
            }
        }

        /// <summary>
        /// Gets the processor architecture (x86, x64, ARM, ARM64, etc.)
        /// </summary>
        /// <returns>The processor architecture</returns>
        private static string GetProcessorArchitecture()
        {
            try
            {
                // Get the process architecture
                var processArch = System.Runtime.InteropServices.RuntimeInformation.ProcessArchitecture;
                
                return processArch switch
                {
                    System.Runtime.InteropServices.Architecture.X86 => "x86",
                    System.Runtime.InteropServices.Architecture.X64 => "x64",
                    System.Runtime.InteropServices.Architecture.Arm => "ARM",
                    System.Runtime.InteropServices.Architecture.Arm64 => "ARM64",
                    System.Runtime.InteropServices.Architecture.Wasm => "WebAssembly",
                    System.Runtime.InteropServices.Architecture.S390x => "S390x",
                    _ => processArch.ToString()
                };
            }
            catch
            {
                // Fallback: use environment variables
                var arch = Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE");
                if (!string.IsNullOrEmpty(arch))
                {
                    return arch;
                }
                
                // Final fallback
                return "Unknown";
            }
        }

        /// <summary>
        /// Returns a legacy minimum date used by some SQL Server logic (1/1/1900 00:00:00).
        /// </summary>
        public static DateTime MinDateValue()
        {
            return new DateTime(1900, 1, 1, 0, 0, 0);
        }

        
    }
}
