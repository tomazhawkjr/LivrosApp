create   function util.fn_split(@data nvarchar(MAX), @delimiter nvarchar(5))
returns @t table (id int identity(1,1), item nvarchar(max))
as
begin
 
  declare @textXML XML;
  --replace the delimiter with xml markup. 
  --this CONVERTs a string item1,item2,item3 into <elm>item1</elm><elm>item2</elm><elm>item3</elm>
  SELECT @textXML = CAST('<elm>' + REPLACE(@data, @delimiter, '</elm><elm>') + '</elm>' AS XML);

  --select the nodes from the xml into table fields
  insert into @t(item)
  select T.col.value('.', 'nvarchar(max)') AS data
    from @textXML.nodes('/elm') T(col)

  return
end