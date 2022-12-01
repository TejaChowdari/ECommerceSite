CREATE view getallorders as 
select tblInvoice.InvoiceId, tblUser.UserId, tblUser.Name as 'user',   tblInvoice.Bill, tblInvoice.Payment
, tblInvoice.InvoiceDate,tblInvoice.Status from tblInvoice
inner join tblUser on tblUser.UserId = tblInvoice.UserId

CREATE view getallorderusers as
select tblInvoice.InvoiceId, tblUser.UserId, tblUser.Name as 'user', 
 tblInvoice.Bill,tblInvoice.Payment, tblInvoice.InvoiceDate , tblInvoice.Status
 from tblInvoice
inner join tblUser on tblUser.UserId = tblInvoice.UserId
where tblInvoice.Status = 1

create view user_invoices as
select tblInvoice.InvoiceId,  tblUser.Name as 'Customer', 
 tblInvoice.Bill,tblInvoice.Payment, tblInvoice.InvoiceDate
 from tblInvoice
inner join tblUser on tblUser.UserId = tblInvoice.UserId
where tblInvoice.UserId = ''

create view vw_getallproducts as
select tblProducts.ProID, tblProducts.Name, tblCategories.Name as 'Category', tblProducts.Description, tblProducts.Unit, tblProducts.Image
from tblProducts
inner join tblCategories on tblCategories.CatId = tblProducts.CatId

select * from getallorders
select * from getallorderusers
select * from user_invoices
select * from vw_getallproducts

Drop view getallorders
Drop view getallorderusers
Drop view user_invoices
Drop view vw_getallproducts