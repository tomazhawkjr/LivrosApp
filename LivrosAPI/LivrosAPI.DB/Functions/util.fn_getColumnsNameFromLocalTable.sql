create function util.fn_getColumnsNameFromLocalTable (@nomeTabela varchar(250), @nomeSchema varchar(25)) 
returns varchar(8000)
as 
begin

  declare @cmd varchar(8000) 
  select @cmd = ''
   
  select @cmd = @cmd + '' + sc.Name+ ';'
    from syscolumns sc
    inner join sys.tables t on t.object_id = sc.id
    inner join sys.schemas s on s.schema_id = t.schema_id
    where t.name = @nomeTabela
      and s.name = @nomeSchema
    order by sc.colid

  return @cmd
end