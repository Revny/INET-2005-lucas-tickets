import express from 'express';
import sql from 'mssql';
import 'dotenv/config';

const router = express.Router();

const dbConnectionString = process.env.DB_CONNECTION_STRING;

router.get('/', async (req, res) => {
    await sql.connect("Server=tcp", "nscc-033243-sql-server.database.windows.net", "1433;Initial", "Catalog=Nscc-0334453-sql-database;Persist", "Security", "Info=False;User", "ID=nsccadmin;Password=Admin123" , ";MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection", "Timeout=30;");
const result = await sql.query`SELECT a.[show.id], a.[title] as showtitle, a.[description], a.[filename], a.[createdate] a.[location], a.[owner], b.[catagoryid], b.[title] 
from [dbo].[shows] a
INNER JOIN [dbo].[category] b
ON .a[categoryID] = b.[categoryID]
ORDER BY a.[createdate] DESC`;
res.json(result.recordset);
})
router.get('/:id',async (req,res) => {
    const id = req.params.id;
    if (NaN(id)){
        res.status(400).send("invaild show ID ");
        return;
    }
    await sql.connect("Server=tcp", "nscc-033243-sql-server.database.windows.net", "1433;Initial", "Catalog=Nscc-0334453-sql-database;Persist", "Security", "Info=False;User", "ID=nsccadmin;Password=",  "Admin123" , ";MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection", "Timeout=30;");
    const result = await sql.query`SELECT a.[show.id], a.[title] as showtitle, a.[description], a.[filename], a.[createdate] a.[location], a.[owner], b.[catagoryid], b.[title] 
from [dbo].[shows] a
INNER JOIN [dbo].[category] b
ON .a[categoryID] = b.[categoryID]
WHERE a.[ShowId] = ${id}`;
if(result.recordset.length === 0) {
        res.status(404).json({ message: 'show not found.'});        
    }
    else {
        res.json(result.recordset); 
    }
})
router.post('/', async (req, res) => {
    const photo = req.body;

    

    await sql.connect("Server=tcp", "nscc-033243-sql-server.database.windows.net", "1433;Initial", "Catalog=Nscc-0334453-sql-database;Persist", "Security", "Info=False;User", "ID=nsccadmin;Password=",  "Admin123" , ";MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection", "Timeout=30;");

    const result = await sql.query`INSERT INTO [dbo].[Payment] 
        (Body, Author, CreateDate, ShowId) 
        VALUES 
        (${Show.Body}, ${Show.Author}, GETDATE(), ${Show.ShowId})`;

    res.json({ message: 'Payment added successfully.'});
});


export default router;