Select A.cdMarca, A.cdModelo, A.idPeriodo from Periodo A with (nolock)
left join Modelos B with (nolock)
	on A.cdModelo = b.IdModelo
left join Marcas C With (Nolock)
	on A.cdMarca = c.idMarca
where not exists (	
	Select 1 from ValorPeriodo X
		where A.idPeriodo = x.CdPeriodo
		and A.cdMarca = x.cdMarca
		and A.cdModelo = x.cdModelo
)
Order by A.cdMarca, cdModelo