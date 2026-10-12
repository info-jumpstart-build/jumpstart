using System;
using System.Collections.Generic;

namespace jumptest
{
    public class BaseTest
    {
        public static string[] firstNames = new string[]
        {
            "John", "Jane", "Michael", "Sarah", "David", "Emily", "Chris", "Anna", "James", "Jessica",
            "Matthew", "Laura", "Daniel", "Samantha", "Robert", "Rachel", "Joshua", "Sophia", "Andrew", "Olivia",
            "Joseph", "Megan", "Kevin", "Linda", "Ryan", "Elizabeth", "Tyler", "Chloe", "Brian", "Kaitlyn",
            "Steven", "Victoria", "Paul", "Natalie", "Mark", "Hannah", "Brandon", "Brittany", "Patrick", "Amanda",
            "Anthony", "Katie", "Nicholas", "Julia", "Zachary", "Isabella", "Jacob", "Grace", "Justin", "Mia"
        };

        public static string[] lastNames = new string[]
        {
            "Smith", "Johnson", "Williams", "Brown", "Jones", "Garcia", "Miller", "Davis", "Martinez", "Lopez",
            "Taylor", "Anderson", "Thomas", "Jackson", "White", "Harris", "Martin", "Thompson", "Young", "Walker",
            "Lee", "Hall", "Allen", "King", "Wright", "Scott", "Torres", "Nguyen", "Hill", "Flores",
            "Green", "Adams", "Nelson", "Baker", "Carter", "Mitchell", "Perez", "Roberts", "Turner", "Phillips",
            "Campbell", "Parker", "Evans", "Edwards", "Collins", "Stewart", "Sanchez", "Morris", "Rogers", "Reed"
        };

        public static string[] companyNames = new string[]
        {
            "TechCorp", "Innovate Solutions", "Global Dynamics", "NextGen Technologies", "Quantum Systems",
            "Fusion Enterprises", "Vertex Innovations", "Synergy Group", "AlphaTech", "Pinnacle Corporation",
            "BlueWave Software", "Summit Networks", "Infinite Solutions", "Apex Solutions", "PrimeTech",
            "DataWorks", "Insight Global", "Velocity Tech", "Centurion Systems", "BrightPath Technologies",
            "ClearView Corp", "Nimbus Technologies", "Silverline Innovations", "EagleEye Solutions", "Orion Technologies",
            "SkyHigh Networks", "AstroTech", "Vanguard Systems", "Zenith Enterprises", "NovaTech",
            "Crystal Systems", "Sunrise Technologies", "Titan Innovations", "Elevate Systems", "Visionary Corp",
            "ProTech Industries", "Echo Systems", "Pioneer Technologies", "Sapphire Solutions", "Phoenix Enterprises",
            "Nexus Networks", "Cloud9 Solutions", "Harmony Tech", "Radiant Innovations", "Virtue Technologies",
            "Stellar Systems", "Arcadia Networks", "OmniTech", "Quantum Vision", "Lunar Solutions", "Dynamic Systems"
        };

        public static string[] phoneNumbers = new string[]
        {
            "555-123-4567", "555-234-5678", "555-345-6789", "555-456-7890", "555-567-8901", "555-678-9012", "555-789-0123", 
            "555-890-1234", "555-901-2345", "555-012-3456", "555-111-2222", "555-222-3333", "555-333-4444", "555-444-5555", 
            "555-555-6666", "555-666-7777", "555-777-8888", "555-888-9999", "555-999-0000", "555-000-1111", "555-121-2121", 
            "555-232-3232", "555-343-4343", "555-454-5454", "555-565-6565", "555-676-7676", "555-787-8787", "555-898-9898", 
            "555-909-0909", "555-111-1234", "555-121-2345", "555-131-3456", "555-141-4567", "555-151-5678", "555-161-6789", 
            "555-171-7890", "555-181-8901", "555-191-9012", "555-202-0123", "555-212-1234", "555-222-2345", "555-232-3456", 
            "555-242-4567", "555-252-5678", "555-262-6789", "555-272-7890", "555-282-8901", "555-292-9012", "555-303-0123", 
            "555-313-1234", "555-323-2345"
        };

        public static string[] addresses = new string[]
        {
            "123 Main St", "456 Oak St", "789 Pine St", "101 Maple Ave", "202 Elm St", "303 Cedar St", "404 Birch Rd", 
            "505 Walnut St", "606 Cherry Ln", "707 Spruce St", "808 Poplar St", "909 Sycamore St", "1010 Aspen St", 
            "1111 Willow St", "1212 Linden St", "1313 Magnolia St", "1414 Cypress St", "1515 Fir St", "1616 Redwood St", 
            "1717 Sequoia St", "1818 Dogwood St", "1919 Hawthorn St", "2020 Beech St", "2121 Juniper St", "2222 Hickory St", 
            "2323 Palm St", "2424 Chestnut St", "2525 Maplewood St", "2626 Laurel St", "2727 Hazel St", "2828 Ash St", 
            "2929 Alder St", "3030 Oakwood St", "3131 Pinecrest St", "3232 Elmwood St", "3333 Cedarwood St", "3434 Birchwood St", 
            "3535 Walnutwood St", "3636 Cherrywood St", "3737 Sprucewood St", "3838 Poplarwood St", "3939 Sycamorewood St", 
            "4040 Aspenwood St", "4141 Willowwood St", "4242 Lindenwood St", "4343 Magnolialand St", "4444 Cypressland St", 
            "4545 Firtree St", "4646 Redwoodtree St", "4747 Sequoiawood St", "4848 Dogwoodland St"
        };

        public static string[] cities = new string[]
        {
            "New York", "Los Angeles", "Chicago", "Houston", "Phoenix", "Philadelphia", "San Antonio", "San Diego", 
            "Dallas", "San Jose", "Austin", "Jacksonville", "Fort Worth", "Columbus", "San Francisco", "Charlotte", 
            "Indianapolis", "Seattle", "Denver", "Washington", "Boston", "El Paso", "Nashville", "Detroit", 
            "Oklahoma City", "Portland", "Las Vegas", "Memphis", "Louisville", "Baltimore", "Milwaukee", "Albuquerque", 
            "Tucson", "Fresno", "Sacramento", "Kansas City", "Mesa", "Atlanta", "Omaha", "Raleigh", "Miami", 
            "Long Beach", "Virginia Beach", "Oakland", "Minneapolis", "Tulsa", "Tampa", "Arlington", "New Orleans", 
            "Wichita", "Cleveland"
        };

        public static string[] states = new string[]
        {
            "Alabama", "Alaska", "Arizona", "Arkansas", "California", "Colorado", "Connecticut", "Delaware", "Florida", 
            "Georgia", "Hawaii", "Idaho", "Illinois", "Indiana", "Iowa", "Kansas", "Kentucky", "Louisiana", "Maine", 
            "Maryland", "Massachusetts", "Michigan", "Minnesota", "Mississippi", "Missouri", "Montana", "Nebraska", 
            "Nevada", "New Hampshire", "New Jersey", "New Mexico", "New York", "North Carolina", "North Dakota", "Ohio", 
            "Oklahoma", "Oregon", "Pennsylvania", "Rhode Island", "South Carolina", "South Dakota", "Tennessee", 
            "Texas", "Utah", "Vermont", "Virginia", "Washington", "West Virginia", "Wisconsin", "Wyoming"
        };
        
        public enum TestDataType
        {
            firstnames,
            lastnames,
            companies,
            phoneNumbers,
            emailAddresses,
            addresses,
            principalnames,
            os_user,
            cities,
            states,
            zipcodes,
            random
        }

        private static Random _random = new Random();
        private static Dictionary<TestDataType, string[]> _dataDictionary = null;

        private static Dictionary<TestDataType, string[]> getTestData()
        {
            // Populate the dictionary in the constructor
            if (_dataDictionary == null)
            {
                _dataDictionary = new Dictionary<TestDataType, string[]>
                {
                    { TestDataType.firstnames, firstNames },
                    { TestDataType.lastnames, lastNames },
                    { TestDataType.companies, companyNames },
                    { TestDataType.phoneNumbers, phoneNumbers },
                    { TestDataType.addresses, addresses },
                    { TestDataType.cities, cities },
                    { TestDataType.states, states }
                };
            }

            return _dataDictionary;

        }


        // Method to retrieve data based on TestDataType
        private static string[] getDataArray(TestDataType type)
        {
            if (_dataDictionary.ContainsKey(type))
            {
                return _dataDictionary[type];
            }
            throw new ArgumentException("Invalid data type");
        }

        private static string getRandomString(string[] array)
        {
            if (array == null || array.Length == 0)
            {
                throw new ArgumentException("Array cannot be null or empty");
            }

            int randomIndex = _random.Next(0, array.Length);
            return array[randomIndex];
        }

        private static string getRandomString()
        {
            string result = "rnd" + getRandomInteger().ToString();
            return result;
        }
        

        public static string getRandomFiveDigitNumber()
        {
            // Generate a random number between 10000 and 99999 (inclusive)
            int randomNumber = _random.Next(10000, 100000);
            return randomNumber.ToString("D5");
        }


        private static int getRandomInteger()
        {
            return _random.Next(0,100000);
        }

        public static double getRandomFloat()
        {
            // Generate a random floating-point value between 0 and 10000
            double randomFloat = _random.NextDouble() * 10000;
            return randomFloat;
        }

        public static decimal getRandomDecimal()
        {
            return Convert.ToDecimal( getRandomFloat() );
        }

        public static object getTestData( BaseObject baseObject, string dataType, TestDataType testDataType)
        {
                object result = string.Empty;
                string baseDataType = dataType;
                if (baseDataType.Contains("("))
                {
                    baseDataType = baseDataType.Substring(0, baseDataType.IndexOf("("));

                }


                if (getTestData().ContainsKey(testDataType))
                {
                    result = getRandomString(getTestData()[testDataType]);
                }
                else
                {
                    if (testDataType == TestDataType.emailAddresses)
                    {
                        string f = baseObject["first_name"].ToString();
                        string l = baseObject["last_name"].ToString();
                        result = f.ToLower() + "." + l.ToLower() + "@example.com";
                    }
                    else if (testDataType == TestDataType.principalnames)
                    {
                        string f = baseObject["first_name"].ToString();
                        string l = baseObject["last_name"].ToString();
                        result = (f.Substring(0,1) + l).ToLower();
                    }
                    else if (testDataType == TestDataType.zipcodes)
                    {
                        result = getRandomFiveDigitNumber();
                    }
                    else if (testDataType == TestDataType.os_user)
                    {
                        result = Environment.UserName;
                    }
                    else
                    {
                        switch (baseDataType.ToUpper())
                        {
                            case "BIGINT":
                                 result = Convert.ToInt64(getRandomInteger());
                                 break;
                            case "INTEGER":
                            case "SMALLINT":
                                result = getRandomInteger();
                                break;
                            case "BYTEA":
                            case "BOOLEAN":
                                result = _random.Next(0,1);
                                break;
                            case "DATE":
                            case "TIMESTAMP":
                            case "TIMESTAMP WITH TIME ZONE":
                            case "TIMESTAMPTZ":
                                result = DateTime.Now;
                                break;
                            case "NUMERIC":
                                result = getRandomDecimal();
                                break;
                            case "DOUBLE PRECISION":
                            case "MONEY":
                            case "REAL":
                                result = getRandomFloat();
                                break;
                            case "CHAR":
                            case "TEXT":
                            case "VARCHAR":
                                result = getRandomString();
                                break;
                            default:
                                throw new Exception("Cannot Generate data for " + baseDataType);
                            
                        }
                    }
                }

                return result;
        }

        
private static Dictionary<string, long> _lastIdMap = null;

        public static void addLastId(string domainObj, long id)
        {
            if (_lastIdMap == null)
            {
                _lastIdMap = new Dictionary<string, long>();
            }

            if (_lastIdMap.ContainsKey(domainObj))
            {
                _lastIdMap[domainObj] = id;
            }
            else
            {
                _lastIdMap.Add(domainObj, id);
            }
        }

        public static long getLastId( string domainObj)
        {
            long result = 0;

            if (_lastIdMap != null)
            {
                if (_lastIdMap.ContainsKey(domainObj))
                {
                    result = _lastIdMap[domainObj];
                }
            }

            return result;
        }

            
        

        

            protected static Stack<TestResultStatus> lastTestResultStatus = new Stack<TestResultStatus>();
            protected static Dictionary<long, TestResultStatus> mapTestResultStatus = new Dictionary<long, TestResultStatus>();
            
            protected static Stack<TestPlan> lastTestPlan = new Stack<TestPlan>();
            protected static Dictionary<long, TestPlan> mapTestPlan = new Dictionary<long, TestPlan>();
            
            protected static Stack<Org> lastOrg = new Stack<Org>();
            protected static Dictionary<long, Org> mapOrg = new Dictionary<long, Org>();
            
<<<<<<< HEAD
            protected static Stack<Principal> lastPrincipal = new Stack<Principal>();
            protected static Dictionary<long, Principal> mapPrincipal = new Dictionary<long, Principal>();
            
=======
>>>>>>> main
            protected static Stack<Operation> lastOperation = new Stack<Operation>();
            protected static Dictionary<long, Operation> mapOperation = new Dictionary<long, Operation>();
            
            protected static Stack<OpRole> lastOpRole = new Stack<OpRole>();
            protected static Dictionary<long, OpRole> mapOpRole = new Dictionary<long, OpRole>();
            
            protected static Stack<CronEvery> lastCronEvery = new Stack<CronEvery>();
            protected static Dictionary<long, CronEvery> mapCronEvery = new Dictionary<long, CronEvery>();
            
            protected static Stack<CronMinute> lastCronMinute = new Stack<CronMinute>();
            protected static Dictionary<long, CronMinute> mapCronMinute = new Dictionary<long, CronMinute>();
            
            protected static Stack<CronHour> lastCronHour = new Stack<CronHour>();
            protected static Dictionary<long, CronHour> mapCronHour = new Dictionary<long, CronHour>();
            
            protected static Stack<CronDom> lastCronDom = new Stack<CronDom>();
            protected static Dictionary<long, CronDom> mapCronDom = new Dictionary<long, CronDom>();
            
            protected static Stack<CronMonth> lastCronMonth = new Stack<CronMonth>();
            protected static Dictionary<long, CronMonth> mapCronMonth = new Dictionary<long, CronMonth>();
            
            protected static Stack<CronDow> lastCronDow = new Stack<CronDow>();
            protected static Dictionary<long, CronDow> mapCronDow = new Dictionary<long, CronDow>();
            
            protected static Stack<NavMenu> lastNavMenu = new Stack<NavMenu>();
            protected static Dictionary<long, NavMenu> mapNavMenu = new Dictionary<long, NavMenu>();
            
            protected static Stack<DataSource> lastDataSource = new Stack<DataSource>();
            protected static Dictionary<long, DataSource> mapDataSource = new Dictionary<long, DataSource>();
            
            protected static Stack<AgentStatus> lastAgentStatus = new Stack<AgentStatus>();
            protected static Dictionary<long, AgentStatus> mapAgentStatus = new Dictionary<long, AgentStatus>();
            
            protected static Stack<OnFailure> lastOnFailure = new Stack<OnFailure>();
            protected static Dictionary<long, OnFailure> mapOnFailure = new Dictionary<long, OnFailure>();
            
            protected static Stack<ExecStatus> lastExecStatus = new Stack<ExecStatus>();
            protected static Dictionary<long, ExecStatus> mapExecStatus = new Dictionary<long, ExecStatus>();
            
            protected static Stack<ServerNodeStatus> lastServerNodeStatus = new Stack<ServerNodeStatus>();
            protected static Dictionary<long, ServerNodeStatus> mapServerNodeStatus = new Dictionary<long, ServerNodeStatus>();
            
            protected static Stack<ScriptType> lastScriptType = new Stack<ScriptType>();
            protected static Dictionary<long, ScriptType> mapScriptType = new Dictionary<long, ScriptType>();
            
            protected static Stack<ServerNodeType> lastServerNodeType = new Stack<ServerNodeType>();
            protected static Dictionary<long, ServerNodeType> mapServerNodeType = new Dictionary<long, ServerNodeType>();
            
            protected static Stack<WorkflowType> lastWorkflowType = new Stack<WorkflowType>();
            protected static Dictionary<long, WorkflowType> mapWorkflowType = new Dictionary<long, WorkflowType>();
            
<<<<<<< HEAD
=======
            protected static Stack<PrincipalStatus> lastPrincipalStatus = new Stack<PrincipalStatus>();
            protected static Dictionary<long, PrincipalStatus> mapPrincipalStatus = new Dictionary<long, PrincipalStatus>();
            
>>>>>>> main
            protected static Stack<TestCase> lastTestCase = new Stack<TestCase>();
            protected static Dictionary<long, TestCase> mapTestCase = new Dictionary<long, TestCase>();
            
            protected static Stack<TestRun> lastTestRun = new Stack<TestRun>();
            protected static Dictionary<long, TestRun> mapTestRun = new Dictionary<long, TestRun>();
            
<<<<<<< HEAD
            protected static Stack<PrincipalOrg> lastPrincipalOrg = new Stack<PrincipalOrg>();
            protected static Dictionary<long, PrincipalOrg> mapPrincipalOrg = new Dictionary<long, PrincipalOrg>();
            
            protected static Stack<OpRoleMap> lastOpRoleMap = new Stack<OpRoleMap>();
            protected static Dictionary<long, OpRoleMap> mapOpRoleMap = new Dictionary<long, OpRoleMap>();
            
            protected static Stack<OpRoleMember> lastOpRoleMember = new Stack<OpRoleMember>();
            protected static Dictionary<long, OpRoleMember> mapOpRoleMember = new Dictionary<long, OpRoleMember>();
            
=======
            protected static Stack<OpRoleMap> lastOpRoleMap = new Stack<OpRoleMap>();
            protected static Dictionary<long, OpRoleMap> mapOpRoleMap = new Dictionary<long, OpRoleMap>();
            
>>>>>>> main
            protected static Stack<Schedule> lastSchedule = new Stack<Schedule>();
            protected static Dictionary<long, Schedule> mapSchedule = new Dictionary<long, Schedule>();
            
            protected static Stack<Sql> lastSql = new Stack<Sql>();
            protected static Dictionary<long, Sql> mapSql = new Dictionary<long, Sql>();
            
            protected static Stack<Script> lastScript = new Stack<Script>();
            protected static Dictionary<long, Script> mapScript = new Dictionary<long, Script>();
            
            protected static Stack<ServerNode> lastServerNode = new Stack<ServerNode>();
            protected static Dictionary<long, ServerNode> mapServerNode = new Dictionary<long, ServerNode>();
            
<<<<<<< HEAD
            protected static Stack<TestResult> lastTestResult = new Stack<TestResult>();
            protected static Dictionary<long, TestResult> mapTestResult = new Dictionary<long, TestResult>();
=======
            protected static Stack<Principal> lastPrincipal = new Stack<Principal>();
            protected static Dictionary<long, Principal> mapPrincipal = new Dictionary<long, Principal>();
>>>>>>> main
            
            protected static Stack<EventService> lastEventService = new Stack<EventService>();
            protected static Dictionary<long, EventService> mapEventService = new Dictionary<long, EventService>();
            
            protected static Stack<Process> lastProcess = new Stack<Process>();
            protected static Dictionary<long, Process> mapProcess = new Dictionary<long, Process>();
            
<<<<<<< HEAD
=======
            protected static Stack<TestResult> lastTestResult = new Stack<TestResult>();
            protected static Dictionary<long, TestResult> mapTestResult = new Dictionary<long, TestResult>();
            
            protected static Stack<PrincipalOrg> lastPrincipalOrg = new Stack<PrincipalOrg>();
            protected static Dictionary<long, PrincipalOrg> mapPrincipalOrg = new Dictionary<long, PrincipalOrg>();
            
            protected static Stack<OpRoleMember> lastOpRoleMember = new Stack<OpRoleMember>();
            protected static Dictionary<long, OpRoleMember> mapOpRoleMember = new Dictionary<long, OpRoleMember>();
            
>>>>>>> main
            protected static Stack<Workflow> lastWorkflow = new Stack<Workflow>();
            protected static Dictionary<long, Workflow> mapWorkflow = new Dictionary<long, Workflow>();
            
            protected static Stack<ExecLog> lastExecLog = new Stack<ExecLog>();
            protected static Dictionary<long, ExecLog> mapExecLog = new Dictionary<long, ExecLog>();
                }
}


