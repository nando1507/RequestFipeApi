--Create Database Fipe
--use fipe

--Create Table Marcas(
--	idMarca int,	
--	NmMarca Varchar(100),
--	NmMarcaFipe Varchar(100),
--	NmKey Varchar(100),	
--	DtCapturaMarcas DateTime Constraint dfDtCapturaMarcas Default getdate(),
--	Constraint PkMarcas Primary key (idMarca, DtCapturaMarcas)
--)

--Create Table Modelos(
--	IdModelo int,
--	NmModelo Varchar(100),
--	NmModeloFipe Varchar(100),
--	NmKey Varchar(100),
--	CdMarca int,
--	DtCapturaModelos DateTime Constraint dfDtCapturaModelos Default getdate(),
--	Constraint PkModelos Primary key (IdModelo, DtCapturaModelos)--,
----	/*Constraint Fk_Marcas*/ foreign key (CdMarca) references Marcas(idMarca)
--)

--Create table Periodo(
--	idPeriodo Varchar(100),
--	NmPeriodo Varchar(100),
--	--NmCodigoFipe Varchar(100),
--	NmKey Varchar(100),
--	cdModelo int,
--	cdMarca int,
--	DtCapturaPeriodo DateTime Constraint dfDtCapturaPeriodo Default getdate(),
--	Constraint PkPeriodo Primary key (idPeriodo, DtCapturaPeriodo)
--)

--Create table ValorPeriodo(
--	idValorPeriodo Varchar(100),
--	NmReferencia Varchar(100),
--	NmFipeCodigo Varchar(100),
--	NmModelo Varchar(100),
--	NmCombustivel Varchar(100),
--	NmMarca Varchar(100),
--	NuAnoModelo Int,
--	VlrPrecoPeriodo	Numeric(15,2),
--	NmKey Varchar(100),
--	HrValorPeriodo float,
--	NmVeiculo Varchar(1000),
--	CdPeriodo Varchar(100),
--	cdModelo int,
--	cdMarca int,
--	DtCapturaValorPeriodo DateTime Constraint dfDtCapturaValorPeriodo Default getdate(),
--	Constraint PkValorPeriodo Primary key (idValorPeriodo, DtCapturaValorPeriodo)
--)


--"referencia": "novembro de 2020", 
--"fipe_codigo": "037001-0", 
--"name": "Hummer Hard-Top 6.5 4x4 Diesel TB", 
--"combustivel": "Diesel", 
--"marca": "AM Gen", 
--"ano_modelo": "1999", 
--"preco": "R$ 187.557,00", 
--"key": "hummer-1999", 
--"time": 0.04000000000007731, 
--"veiculo": "Hummer Hard-Top 6.5 4x4 Diesel TB", 
--"id": "1999"



--Select *from Periodo
--where cdmarca = 6 and cdmodelo = 134
--order by cdMarca desc, cdmodelo desc, idperiodo



--Select Distinct idMarca from Marcas where cast(DtCapturaMarcas as date) = cast(getdate() as date) Order by idMarca

--Select Distinct IdModelo, CdMarca from Modelos With (nolock) --Order by  IdModelo, CdMarca
--where cdmarca = 6 and idmodelo = 134
--where cdMarca = 208

--Select idMarca from Marcas
--where idMarca = 214

--Select Distinct idMarca from Marcas where cast(DtCapturaMarcas as date) = cast(getdate() as date) Order by idMarca



--Select A.cdMarca, cdModelo, idPeriodo from Periodo A with (nolock)
--left join Modelos B with (nolock)
--	on A.cdModelo = b.IdModelo
--left join Marcas C With (Nolock)
--	on A.cdMarca = c.idMarca
--Order by A.cdMarca, cdModelo


--{"referencia": "novembro de 2020", 
--"fipe_codigo": "038003-2",
--"name": "Integra GS 1.8", 
--"combustivel": "Gasolina", 
--"marca": "Acura", 
--"ano_modelo": "1992", 
--"preco": "R$ 11.021,00", 
--"key": "integra-1992", 
--"time": 0.009999999999990905, 
--"veiculo": "Integra GS 1.8", 
--"id": "1992"}


Select* from Modelos A With (Nolock)
inner join Marcas B With (Nolock)
	on a.CdMarca = b.idMarca	
left join Periodo C With (Nolock)
	on A.CdMarca = C.cdMarca
	and A.IdModelo = c.cdModelo
left join ValorPeriodo D With (Nolock)
	on D.cdMarca = C.cdmarca
	and D.CdModelo = C.cdModelo
	and D.cdPeriodo = C.idPeriodo
where b.NmMarca like '%volks%'
and A.NmModelo like '%Fox%plus%'


Select* from Modelos A With (Nolock)
inner join Marcas B With (Nolock)
	on a.CdMarca = b.idMarca	
left join Periodo C With (Nolock)
	on A.CdMarca = C.cdMarca
	and A.IdModelo = c.cdModelo
left join ValorPeriodo D With (Nolock)
	on D.cdMarca = C.cdmarca
	and D.CdModelo = C.cdModelo
	and D.cdPeriodo = C.idPeriodo
where b.NmMarca like '%volks%'
and A.NmModelo like '%Fox%plus%'
and c.NmPeriodo like '%2006%'




Select A.cdMarca, cdModelo, idPeriodo from Periodo A with (nolock)
                                                    left join Modelos B with (nolock)
	                                                    on A.cdModelo = b.IdModelo
                                                    left join Marcas C With (Nolock)
	                                                    on A.cdMarca = c.idMarca
                                                    Order by A.cdMarca, cdModelo



													Select * from ValorPeriodo


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