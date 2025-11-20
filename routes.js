import express from 'express';
import sql from 'mssql';
import 'dotenv/config';

const router = express.Router();

const dbConnectionString = process.env.DB_CONNECTION_STRING;

router.get('/', async (req, res) => {
    await sql.connect(dbConnectionString);
const result = await sql.query`SELECT a.[Show.Id], a.[Title], a.[Description], a.[Filename], a.[Createdate] [Location], a.[Owner], b.[CatagoryID], b.[Title] 
from [dbo].[shows] a
INNER JOIN [dbo].[category] b
ON .a[CategoryID] = b.[CategoryID]
ORDER BY a.[createdate] DESC`;
res.json(result.recordset);
})
router.get('/:id',async (req,res) => {
    const id = req.params.id;
    if (NaN(id)){
        res.status(400).send("invaild show ID ");
        return;
    }
    await sql.connect(dbConnectionString);
    const result = await sql.query`SELECT a.[Show.Id], a.[Title], a.[Description], a.[Filename], a.[Createdate] [Location], a.[Owner], b.[CatagoryID], b.[Title] 
from [dbo].[shows] a
INNER JOIN [dbo].[Category] b
ON .a[CategoryID] = b.[CategoryID]
WHERE a.[ShowId] = ${id}`;
if(result.recordset.length === 0) {
        res.status(404).json({ message: 'show not found.'});        
    }
    else {
        res.json(result.recordset); 
    }
})
router.post('/', async (req, res) => {
    const show = req.body;

    

    await sql.connect(dbConnectionString);

    const result = await sql.query`INSERT INTO [dbo].[Payment] 
        (Body, Author, CreateDate, ShowID) 
        VALUES 
        (${show.Body}, ${show.Author}, GETDATE(), ${show.ShowID})`;

    res.json({ message: 'Payment added successfully.'});
});


export default router;