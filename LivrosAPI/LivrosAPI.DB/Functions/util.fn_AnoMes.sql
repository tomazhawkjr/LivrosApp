create function util.fn_AnoMes(@Data date)
returns varchar(10)
as
begin
  declare @retorno varchar(10)
   
  if @Data is not null begin
    select @retorno = format(@Data,'MMM/yyyy','pt-BR')
    select @retorno = concat(upper(substring(@retorno,1,1)),substring(@retorno,2,len(@retorno)-1))
  end
   
   return @retorno
end