module Model.Connection
open FSharp.Data.Sql
open System.IO

let [<Literal>] private dataSourceName = "mqprodb.mdb"
let [<Literal>] private dataSource = @"D:\" + dataSourceName
let [<Literal>] private connectionString = 
    "Data Source=" + dataSource + ";Provider=Microsoft.ACE.OLEDB.12.0"

// Install DB if not exists
do (
    if not (File.Exists dataSource) then
        printfn "未发现数据库文件..."
        File.Copy (dataSourceName,dataSource)
        printfn "已将默认数据库文件复制到%s。" dataSource)

type SqlConnection = 
    SqlDataProvider< 
        ConnectionString = connectionString,
        DatabaseVendor = Common.DatabaseProviderTypes.MSACCESS>

let SqlContext = SqlConnection.GetDataContext()
let Database = SqlContext.Mqprodb

