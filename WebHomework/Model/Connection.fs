module Model.Connection
open FSharp.Data.Sql
open System.IO

let [<Literal>] private dataSourceName = "mqprodb.mdb"
let [<Literal>] private dataSource = @"D:\" + dataSourceName
let [<Literal>] private connectionString = 
    "Data Source=" + dataSource + ";Provider=Microsoft.ACE.OLEDB.12.0"

type SqlConnection = 
    SqlDataProvider< 
        ConnectionString = connectionString,
        DatabaseVendor = Common.DatabaseProviderTypes.MSACCESS>

let SqlContext = SqlConnection.GetDataContext()
let Database = SqlContext.Mqprodb

