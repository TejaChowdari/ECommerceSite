 --overall order's
create view getallorders as 
select tblInvoice.InvoiceId,tblUser.UserId, tblUser.Name as 'user', tblInvoice.Bill,tblInvoice.Payment, tblInvoice.InvoiceDate
--,tblInvoice.Status 
from tblInvoice
inner join tblUser on tblUser.UserId = tblInvoice.UserId

--user's order
create view getallorderusers as
select tblInvoice.InvoiceId,tblUser.UserId, tblUser.Name as 'user', tblInvoice.Bill,tblInvoice.Payment, tblInvoice.InvoiceDate
--,tblInvoice.Status  
from tblInvoice
inner join tblUser on tblUser.UserId = tblInvoice.UserId
--where tblInvoice.Status = 1

/* get invoice for user  */
create view user_invoices as
select tblInvoice.InvoiceId,  tblUser.Name as 'Customer', 
 tblInvoice.Bill,tblInvoice.Payment, tblInvoice.InvoiceDate
 from tblInvoice
inner join tblUser on tblUser.UserId = tblInvoice.UserId
where tblInvoice.UserId = ''

/* get all product for admin  */
--create view vw_getallproducts as
create view getallproducts as
select tblProducts.ProID, tblProducts.Name, tblCategories.Name as 'Category', tblProducts.Description, tblProducts.Unit, tblProducts.Image
from tblProducts
inner join tblCategories on tblCategories.CatId = tblProducts.CatId

/* call for view */
select * from getallorders
select * from getallorderusers
select * from user_invoices
--select * from vw_getallproducts
select * from getallproducts

/* in case any changes or drop view */
drop view getallorders
drop view getallorderusers
drop view user_invoices
drop view vw_getallproducts