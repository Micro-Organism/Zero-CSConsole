using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Management.Smo.RegisteredServers;//在microsoft.sqlserver.smo.dll中
using Microsoft.SqlServer.Management.Smo;//需添加microsoft.sqlserver.smo.dll的引用
using Microsoft.SqlServer.Management.Common;//需添加microsoft.sqlserver.connectioninfo.dll的引用
using System.IO;

namespace Zero.DataBases.SQLSERVER
{
    public class SMOHelper
    {
        #region 单例模式
        private static SMOHelper _Instance;
        public static SMOHelper Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new SMOHelper();
                }
                return _Instance;
            }
        }
        #endregion

        #region 字段属性
        private static Server SMOServer { get; set; }
        #endregion

        #region 静态方法

        /// <summary>
        /// 创建数据库连接
        /// </summary>
        public static void Connect()
        {
            try
            {
                if (SMOServer == null)
                {
                    string connectionString = @"Data Source=.\ACALSQLEXPRESS;Initial Catalog=master;User ID=sa;Password=ACal@Server123456;";

                    //创建ServerConnection的实例
                    ServerConnection connection = new ServerConnection();
                    //指定连接字符串
                    connection.ConnectionString = connectionString;
                    //实例化Server
                    SMOServer = new Server(connection);
                    //连接数据库
                    SMOServer.ConnectionContext.Connect();
                    //Disable automatic disconnection. 
                    SMOServer.ConnectionContext.AutoDisconnectMode = AutoDisconnectMode.NoAutoDisconnect;
                }
            }
            catch (Exception ex)
            {
                Disconnect();
            }
        }

        /// <summary>
        /// 断开数据库连接
        /// </summary>
        public static void Disconnect()
        {
            try
            {
                if (SMOServer != null)
                {
                    //Disconnect explicitly. 
                    SMOServer.ConnectionContext.Disconnect();
                }
            }
            catch(Exception exc)
            {
                Console.WriteLine(exc.Message);
            }
        }

        /// <summary>
        /// 显示数据库常见对象信息示例，本方法中的代码只针对9.0版本的SMO dll(SQL2005附带程序集）
        /// </summary>
        public static void ShowSMOObjects()
        {
            Console.WriteLine("Server Group Information");
            foreach (ServerGroup serverGroup in SqlServerRegistrations.ServerGroups)
            {
                Console.WriteLine("Group Name:{0},Path:{1},ServerType:{2},State:{3},Urn:{4}", serverGroup.Name, serverGroup.Path, serverGroup.ServerType, serverGroup.State, serverGroup.Urn);
            }

            Console.WriteLine("Registered Server Information");
            foreach (RegisteredServer regServer in SqlServerRegistrations.RegisteredServers)
            {
                Console.WriteLine("Server Name:{0},Login:{1},State:{2},Urn:{3}", regServer.Name, regServer.Login, regServer.State, regServer.Urn);
            }

            //创建ServerConnection的实例
            ServerConnection connection = new ServerConnection();
            //指定连接字符串
            connection.ConnectionString = @"Data Source=.\ACALSQLEXPRESS;Initial Catalog=master;User ID=sa;Password=ACal@Server123456;";
            //"Data Source=goodapp;Initial Catalog=master;User ID=sa;Password=root;";
            //实例化Server
            Server server = new Server(connection);
            //Console.WriteLine("ActiveDirectory:{0},InstanceName:{1}", server.ActiveDirectory, server.InstanceName);
            Console.WriteLine("InstanceName:{0}", server.InstanceName);
            //下面列出每个数据库的具体信息
            foreach (Database db in server.Databases)
            {
                Console.WriteLine("Database Name:{0},ActiveConnections:{1},DataSpaceUsage:{2},PrimaryFilePath:{3}", db.Name, db.ActiveConnections, db.DataSpaceUsage, db.PrimaryFilePath);
                //Console.WriteLine("Database Name:{0},ActiveDirectory:{1},ActiveConnections:{2},DataSpaceUsage:{3},PrimaryFilePath:{4}", db.Name, db.ActiveDirectory, db.ActiveConnections, db.DataSpaceUsage, db.PrimaryFilePath);
                //列出数据库的数据文件文件组信息
                foreach (FileGroup fileGroup in db.FileGroups)
                {
                    Console.WriteLine("\tFileGroup Name:{0},Size:{1},State:{2},Urn:{3}", fileGroup.Name, fileGroup.Size, fileGroup.State, fileGroup.Urn);
                    //列出每个文件组中的数据文件信息
                    foreach (DataFile dataFile in fileGroup.Files)
                    {
                        Console.WriteLine("\t\tDataFile Name:{0},Size:{1},State:{2},Urn:{3},FileName:{4}", dataFile.Name, dataFile.Size, dataFile.State, dataFile.Urn, dataFile.FileName);
                    }
                }
                //列出数据库日志文件信息
                foreach (LogFile logFile in db.LogFiles)
                {
                    Console.WriteLine("\tLogFile Name:{0},Size:{1},State:{2},Urn:{3},FileName:{4}", logFile.Name, logFile.Size, logFile.State, logFile.Urn, logFile.FileName);
                }
            }
        }

        /// <summary>
        /// 利用SMO创建SQL登录
        /// </summary>
        public static void CreateLogin()
        {
            string loginName = "zhoufoxcn";//要创建的数据库登录名
            string loginPassword = "C#.NET";//登录密码

            //创建ServerConnection的实例
            ServerConnection connection = new ServerConnection();
            //指定连接字符串
            connection.ConnectionString = "Data Source=goodapp;Initial Catalog=master;User ID=sa;Password=root;";
            //实例化Server
            Server server = new Server(connection);

            #region [创建数据库登录对象]
            //检查在数据库是否已经存在该登录名
            var queryLogin = from Login temp in server.Logins
                             where string.Equals(temp.Name, loginName, StringComparison.CurrentCultureIgnoreCase)
                             select temp;
            Login login = queryLogin.FirstOrDefault<Login>();
            //如果存在就删除
            if (login != null)
            {
                login.Drop();
            }
            login = new Login(server, loginName);
            login.LoginType = LoginType.SqlLogin;//指定登录方式为SQL认证
            login.PasswordPolicyEnforced = true;
            login.DefaultDatabase = "master";//默认数据库
            login.Create(loginPassword);
            #endregion
        }

        /// <summary>
        /// 利用SMO创建数据库
        /// </summary>
        public static void CreateDatabase(string databaseName, string dbPath)
        {
            try
            {
                //创建ServerConnection的实例
                ServerConnection connection = new ServerConnection();
                //指定连接字符串
                connection.ConnectionString = @"Data Source=.\ACALSQLEXPRESS;Initial Catalog=master;User ID=sa;Password=ACal@Server123456;";
                //"Data Source=goodapp;Initial Catalog=master;User ID=sa;Password=root;";
                //实例化Server
                Server server = new Server(connection);

                #region [创建数据库对象]
                //检查在数据库是否已经存在该数据库
                var queryDatabase = from Database temp in server.Databases
                                    where string.Equals(temp.Name, databaseName, StringComparison.CurrentCultureIgnoreCase)
                                    select temp;
                Database database = queryDatabase.FirstOrDefault<Database>();
                //如果存在就删除
                if (database != null)
                {
                    database.Drop();
                }

                database = new Database(server, databaseName);
                //指定数据库数据文件细节
                FileGroup fileGroup = new FileGroup { Name = "PRIMARY", Parent = database, IsDefault = false };
                DataFile dataFile = new DataFile
                {
                    Name = databaseName + "_data",
                    Parent = fileGroup,
                    FileName = dbPath + databaseName + ".mdf"
                };
                fileGroup.Files.Add(dataFile);
                //指定数据库日志文件细节
                LogFile logFile = new LogFile
                {
                    Name = databaseName + "_log",
                    Parent = database,
                    FileName = dbPath + databaseName + ".ldf"
                };

                database.FileGroups.Add(fileGroup);
                database.LogFiles.Add(logFile);

                database.Create();
                #endregion
            }
            catch (Exception exc)
            {
                var message = exc.Message;
                Console.WriteLine(exc.ToString());
            }
            finally
            {
                //Console.ReadKey();
            }

        }

        /// <summary>
        /// 利用SMO删除数据库
        /// </summary>
        public static void DeleteDatabase(string databaseName)
        {
            try
            {
                //创建ServerConnection的实例
                ServerConnection connection = new ServerConnection();
                //指定连接字符串
                connection.ConnectionString = @"Data Source=.\ACALSQLEXPRESS;Initial Catalog=master;User ID=sa;Password=ACal@Server123456;";
                //"Data Source=goodapp;Initial Catalog=master;User ID=sa;Password=root;";
                //实例化Server
                Server server = new Server(connection);

                #region [创建数据库对象]
                //检查在数据库是否已经存在该数据库
                var queryDatabase = from Database temp in server.Databases
                    where string.Equals(temp.Name, databaseName, StringComparison.CurrentCultureIgnoreCase)
                    select temp;
                Database database = queryDatabase.FirstOrDefault<Database>();

                //如果存在就删除
                if (database != null)
                {
                    database.Drop();
                }
                #endregion
            }
            catch (Exception exc)
            {
                var message = exc.Message;
                Console.WriteLine(exc.ToString());
            }
            finally
            {
                //Console.ReadKey();
            }

        }

        /// <summary>
        /// 利用SMO备份数据库
        /// </summary>
        public static void BackupDatabase()
        {
            string databaseName = "msdb";//备份的数据库名
            string bkPath = @"C:\";//存放备份后的数据的文件夹
            //创建ServerConnection的实例
            ServerConnection connection = new ServerConnection();
            //指定连接字符串
            connection.ConnectionString = "Data Source=goodapp;Initial Catalog=master;User ID=sa;Password=root;";
            //实例化Server
            Server server = new Server(connection);

            #region [创建数据库备份对象]
            Backup backup = new Backup();
            backup.Action = BackupActionType.Database;//完全备份
            backup.Database = databaseName;
            backup.BackupSetDescription = "Full backup of master";
            backup.BackupSetName = "master Backup";
            //创建备份设备
            BackupDeviceItem bkDeviceItem = new BackupDeviceItem();
            bkDeviceItem.DeviceType = DeviceType.File;
            bkDeviceItem.Name = bkPath + databaseName + ".bak";

            backup.Devices.Add(bkDeviceItem);
            backup.Incremental = false;
            backup.LogTruncation = BackupTruncateLogType.Truncate;
            backup.SqlBackup(server);
            #endregion
        }

        /// <summary>
        /// 利用SMO还原数据库
        /// </summary>
        public static void RestoreDatabase()
        {
            try
            {
                string databaseName = "SMODemo";//备份的数据库名
                string bkPath = @"D:\ConSTACalData\backup\";//存放备份后的数据的文件夹
                string rsPath = @"D:\TEMP\";//存放备份后的数据的文件夹

                //创建数据库
                //CreateDatabase(databaseName, rsPath);
                //SMOHelper.Instance.CreateDatabase(databaseName, rsPath, false);
                DeleteDatabase(databaseName);

                //创建ServerConnection的实例
                ServerConnection connection = new ServerConnection();
                //指定连接字符串
                connection.ConnectionString = @"Data Source=.\ACALSQLEXPRESS;Initial Catalog=master;User ID=sa;Password=ACal@Server123456;";
                //实例化Server
                Server server = new Server(connection);

                Restore restore = new Restore();
                restore.NoRecovery = false;
                restore.NoRewind = false;
                restore.Action = RestoreActionType.Files;
                restore.Database = databaseName;

                //创建备份设备
                BackupDeviceItem bkDeviceItem = new BackupDeviceItem();
                bkDeviceItem.DeviceType = DeviceType.File;
                bkDeviceItem.Name = bkPath + databaseName + ".bak";

                //如果需要重新制定Restore后的数据库的物理文件位置，需要知道数据库文件的逻辑文件名
                //可以RESTORE FILELISTONLY 来列出逻辑文件名，如果覆盖已有数据库可以通过SMO来获取
                //因本处使用的是刚刚备份的msdb数据库来Restore，所以其分别为"MSDBData"和"MSDBLog"
                //如果不指定Restore路径则默认恢复到数据库服务器存放数据的文件夹下

                var dataFileName = "ConSTACal" + "_Data";
                var logFileName = "ConSTACal" + "_Log";

                RelocateFile relocateDataFile = new RelocateFile { LogicalFileName = dataFileName, PhysicalFileName = rsPath + databaseName + ".mdf" };//(databaseName + "_data", bkPath + databaseName + ".mdf");
                RelocateFile relocateLogFile = new RelocateFile { LogicalFileName = logFileName, PhysicalFileName = rsPath + databaseName + ".ldf" };//(databaseName + "_log", bkPath + databaseName + ".ldf");

                restore.Devices.Add(bkDeviceItem);
                restore.RelocateFiles.Add(relocateDataFile);
                restore.RelocateFiles.Add(relocateLogFile);
                restore.SqlRestore(server);
                Console.WriteLine("还原成功");
            }
            catch (Exception exc)
            {
                var message = exc.Message;
                Console.WriteLine(exc.ToString());
            }
            finally 
            {
                Console.ReadKey();
            }

        }
        #endregion

        #region 基本方法
        /// <summary>
        /// 创建数据库
        /// </summary>
        /// <param name="overwrite">删除已经存在的数据库</param>
        public void CreateDatabase(string databaseName, string Path, bool overwrite = false)
        {
            try
            {
                if (databaseName == null || databaseName.Trim() == string.Empty)
                {
                    throw new ArgumentNullException("databaseName");
                }
                if (Path == null || Path.Trim() == string.Empty)
                {
                    throw new ArgumentNullException("Path");
                }
                if (!Directory.Exists(Path))
                {
                    throw new InvalidArgumentException(Path);
                }
                if (Path[Path.Length - 1] != '\\')
                {
                    Path += '\\';
                }

                Connect();

                #region [创建数据库对象]
                //检查在数据库是否已经存在该数据库
                Database database = SMOServer.Databases[databaseName];
                if (database != null)
                {
                    //如果存在就删除
                    if (overwrite)
                    {
                        database.Drop();
                    }
                    else
                    {
                        throw new InvalidDataException(databaseName);
                    }
                }

                database = new Database(SMOServer, databaseName);
                //指定数据库数据文件细节
                FileGroup fileGroup = new FileGroup {Name= "PRIMARY", Parent = database, IsDefault = false };
                DataFile dataFile = new DataFile
                {
                    Name = databaseName,//+ "_Data"
                    Parent = fileGroup,
                    FileName = Path + databaseName + ".mdf"
                };
                fileGroup.Files.Add(dataFile);
                //指定数据库日志文件细节
                LogFile logFile = new LogFile
                {
                    Name = databaseName + "_Log",
                    Parent = database,
                    FileName = Path + databaseName + "_log.ldf"
                };

                database.FileGroups.Add(fileGroup);
                database.LogFiles.Add(logFile);

                database.Create();
                #endregion
            }
            catch (Exception ex)
            {
                Disconnect();
            }
        }
        #endregion

    }
}
