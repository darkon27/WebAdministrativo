DELETE FROM SCI_PERSONA	WHERE IDPERSONA=45

SELECT * FROM SCI_USUARIO
SELECT * FROM SCI_TAREAS
SELECT * FROM SCI_MAESTRODETALLE
SELECT * FROM SCI_TICKET
--SELECT  * FROM SCI_MAESTRODETALLE
--SELECT  * FROM SCI_USUARIO
--SELECT  * FROM SCI_TAREAS
--SELECT  * FROM SCI_ACCIONES
--SELECT  * FROM SCI_ALERTAS
--select * from SCI_TICKET
--select * from SCI_MAESTRODETALLE
SELECT  * FROM SCI_USUARIO
SELECT  * FROM VIEW_Accesos
SELECT  * FROM SCI_PERSONA
SELECT  * FROM [VIEW_Persona]




update SCI_PERSONA set FECHACREACION=GETDATE(), USUARIO='solis' ,PASSWORD=123 where IDUSUARIO=37
--GO

ALTER TABLE SCI_PERSONA ADD  TIPOPERSONA VARCHAR(1) NULL;
ALTER TABLE SCI_PERSONA ADD  SEXO VARCHAR(1) NULL;
ALTER TABLE SCI_PERSONA ADD  ESTADOCIVIL VARCHAR(1) NULL;
ALTER TABLE SCI_PERSONA ADD  RUTAFIRMA VARCHAR(250) NULL;


--EXEC SCI_ListarTarea 
--DROP TABLE SCI_ACCIONES
--DROP TABLE SCI_TAREAS
--DROP TABLE SCI_REQUERIMIENTO
GO


ALTER VIEW [dbo].[VIEW_Persona]
AS
	SELECT  TPE.NOMBRE DESTIPOPERSONA,TDO.NOMBRE DESTIPODOCUMENTO,TCI.NOMBRE DESESTADOCIVIL,TSE.NOMBRE DESSEXO,U.* , 
	case U.estado when 1 then 'Activo' else 'Inactivo' end DesEstado,0 Edad, '' Departamento,'' Provincia, '' Distrito
	FROM SCI_PERSONA U WITH(NOLOCK)
	LEFT join  SCI_MAESTRODETALLE TPE  WITH(NOLOCK) ON TPE.IDMAESTRO=2 AND   U.TIPOPERSONA   = TPE.CODIGO 
	LEFT join  SCI_MAESTRODETALLE TDO  WITH(NOLOCK) ON TDO.IDMAESTRO=3 AND   U.TIPODOCUMENTO = TDO.CODIGO 
	LEFT join  SCI_MAESTRODETALLE TCI  WITH(NOLOCK) ON TCI.IDMAESTRO=4 AND   U.ESTADOCIVIL   = TCI.CODIGO 
	LEFT join  SCI_MAESTRODETALLE TSE  WITH(NOLOCK) ON TSE.IDMAESTRO=7 AND   U.SEXO   = TSE.CODIGO 

GO


CREATE VIEW [dbo].[VIEW_Maestro]
AS
	SELECT  *,case estado when 1 then 'Activo' else 'Inactivo' end DesEstado
	FROM SCI_MAESTRO

GO

CREATE VIEW [dbo].[VIEW_MaestroDetalle]
AS
	SELECT  TMD.*,case TMD.estado when 1 then 'ACTIVO' else 'INACTIVO' end DesEstado,F.DESCRIPCION COLORESTADO,TM.CODIGO CodMaestro,TM.NOMBRE NomMaestro
	FROM SCI_MAESTRODETALLE  TMD
	INNER JOIN SCI_MAESTRO TM	ON	 TM.IdMaestro = TMD.IdMaestro
	LEFT JOIN SCI_MAESTRODETALLE F WITH(NOLOCK) ON  F.CODIGO=TMD.ESTADO AND F.IDMAESTRO=22
GO

CREATE VIEW [dbo].[VIEW_Alerta]
AS
	SELECT B.NOMBRECOMPLETO SOLICITANTE, C.NOMBRECOMPLETO RESPONSABLE,C.EMAIL,
	E.NOMBRE TIPOREQUERIMIENTO, F.NOMBRE DESESTADO,F.DESCRIPCION COLORESTADO, H.NOMBRE AREA,CONVERT(VARCHAR,a.FECHAREGISTRO,103) FECREGISTRO,
	CONVERT(VARCHAR,a.FECHAINIPLAZO,103) FECINIPLAZO, CONVERT(VARCHAR,a.FECHAFINPLAZO,103) FECFINPLAZO ,  I.* 
	FROM SCI_ALERTAS  I WITH(NOLOCK)  
	INNER JOIN SCI_TAREAS A WITH(NOLOCK) ON  I.IDRELACIONADO=A.ID_TAREA  AND I.TIPOALERTA=1
	LEFT JOIN SCI_PERSONA B WITH(NOLOCK) ON  B.IDPERSONA=A.ID_SOLICITANTE
	LEFT JOIN SCI_MAESTRODETALLE E WITH(NOLOCK) ON  E.CODIGO=A.TIPO AND E.IDMAESTRO=21
	LEFT JOIN SCI_MAESTRODETALLE F WITH(NOLOCK) ON  F.CODIGO=A.ESTADO AND F.IDMAESTRO=22
	LEFT JOIN SCI_PERSONA C WITH(NOLOCK) ON  C.IDPERSONA=I.IDPERSONA
	LEFT JOIN SCI_MAESTRODETALLE H WITH(NOLOCK) ON  H.CODIGO=A.IDAREA AND H.IDMAESTRO=24

GO
CREATE VIEW [dbo].[VIEW_TareaAsignadas]
AS

	SELECT B.NOMBRECOMPLETO SOLICITANTE, C.NOMBRECOMPLETO RESPONSABLE,D.NOMBRECOMPLETO ALTERNO,G.NOMBRECOMPLETO INVOLUCRADO,
	E.NOMBRE TIPOREQUERIMIENTO, F.NOMBRE DESESTADO,F.DESCRIPCION COLORESTADO, H.NOMBRE AREA,CONVERT(VARCHAR,a.FECHAREGISTRO,103) FECREGISTRO,
	CONVERT(VARCHAR,a.FECHAINIPLAZO,103) FECINIPLAZO, CONVERT(VARCHAR,a.FECHAFINPLAZO,103) FECFINPLAZO ,  A.* 
	FROM SCI_TAREAS A WITH(NOLOCK)  
	INNER JOIN SCI_ALERTAS I WITH(NOLOCK) ON  I.IDRELACIONADO=A.ID_TAREA  AND I.TIPOALERTA=1
	LEFT JOIN SCI_PERSONA B WITH(NOLOCK) ON  B.IDPERSONA=A.ID_SOLICITANTE
	LEFT JOIN SCI_MAESTRODETALLE E WITH(NOLOCK) ON  E.CODIGO=A.TIPO AND E.IDMAESTRO=21
	LEFT JOIN SCI_MAESTRODETALLE F WITH(NOLOCK) ON  F.CODIGO=A.ESTADO AND F.IDMAESTRO=22
	LEFT JOIN SCI_PERSONA C WITH(NOLOCK) ON  C.IDPERSONA=A.ID_RESPONSABLE
	LEFT JOIN SCI_PERSONA D WITH(NOLOCK) ON  D.IDPERSONA=A.ID_ALTERNO
	LEFT JOIN SCI_PERSONA G WITH(NOLOCK) ON  G.IDPERSONA=A.ID_INVOLUCRADO
	LEFT JOIN SCI_MAESTRODETALLE H WITH(NOLOCK) ON  H.CODIGO=A.IDAREA AND H.IDMAESTRO=24

GO

CREATE VIEW [dbo].[VIEW_Tarea]
AS
	SELECT B.NOMBRECOMPLETO SOLICITANTE, C.NOMBRECOMPLETO RESPONSABLE,D.NOMBRECOMPLETO ALTERNO,G.NOMBRECOMPLETO INVOLUCRADO,
	E.NOMBRE TIPOREQUERIMIENTO, F.NOMBRE DESESTADO,F.DESCRIPCION COLORESTADO, H.NOMBRE AREA,CONVERT(VARCHAR,a.FECHAREGISTRO,103) FECREGISTRO,
	CONVERT(VARCHAR,a.FECHAINIPLAZO,103) FECINIPLAZO, CONVERT(VARCHAR,a.FECHAFINPLAZO,103) FECFINPLAZO ,  A.* 
	FROM SCI_TAREAS A WITH(NOLOCK)  
	LEFT JOIN SCI_PERSONA B WITH(NOLOCK) ON  B.IDPERSONA=A.ID_SOLICITANTE
	LEFT JOIN SCI_MAESTRODETALLE E WITH(NOLOCK) ON  E.CODIGO=A.TIPO AND E.IDMAESTRO=21
	LEFT JOIN SCI_MAESTRODETALLE F WITH(NOLOCK) ON  F.CODIGO=A.ESTADO AND F.IDMAESTRO=22
	LEFT JOIN SCI_PERSONA C WITH(NOLOCK) ON  C.IDPERSONA=A.ID_RESPONSABLE
	LEFT JOIN SCI_PERSONA D WITH(NOLOCK) ON  D.IDPERSONA=A.ID_ALTERNO
	LEFT JOIN SCI_PERSONA G WITH(NOLOCK) ON  G.IDPERSONA=A.ID_INVOLUCRADO
	LEFT JOIN SCI_MAESTRODETALLE H WITH(NOLOCK) ON  H.CODIGO=A.IDAREA AND H.IDMAESTRO=24

GO

CREATE VIEW [dbo].[VIEW_Accion]
AS
SELECT B.NOMBRECOMPLETO RESPONSABLE,F.NOMBRE DESESTADO ,CONVERT(VARCHAR,a.FECHA,103) FECREGISTRO,
	F.DESCRIPCION COLORESTADO,A.* 
	FROM SCI_ACCIONES A WITH(NOLOCK)
	LEFT JOIN  SCI_PERSONA B		WITH(NOLOCK) ON  B.IDPERSONA=A.ID_RESPONSABLE
	INNER JOIN SCI_TAREAS C			WITH(NOLOCK) ON  C.ID_TAREA=A.ID_TAREA 
	LEFT JOIN  SCI_MAESTRODETALLE F WITH(NOLOCK) ON  F.CODIGO=A.ESTADO AND F.IDMAESTRO=22

GO


CREATE VIEW [dbo].[VIEW_Ticket]
AS
	SELECT B.NOMBRECOMPLETO SOLICITANTE, C.NOMBRECOMPLETO RESPONSABLE,
	E.NOMBRE TIPOREQUERIMIENTO, F.NOMBRE DESESTADO,F.DESCRIPCION COLORESTADO, CONVERT(VARCHAR,a.FECHAREGISTRO,103) FECHAREGISTRO,
	CONVERT(VARCHAR,a.FECHAINI,103) FECINIPLAZO, CONVERT(VARCHAR,a.FECHAFIN,103) FECFINPLAZO ,  A.* 
	FROM SCI_TICKET A WITH(NOLOCK)  
	LEFT JOIN SCI_PERSONA B WITH(NOLOCK)		ON  B.IDPERSONA=A.IDSOLICITANTE
	LEFT JOIN SCI_MAESTRODETALLE E WITH(NOLOCK) ON  E.CODIGO=A.TIPO AND E.IDMAESTRO=21
	LEFT JOIN SCI_MAESTRODETALLE F WITH(NOLOCK) ON  F.CODIGO=A.ESTADO AND F.IDMAESTRO=22
	LEFT JOIN SCI_PERSONA C WITH(NOLOCK)		ON  C.IDPERSONA=A.IDRESPONSABLE
	
GO


	SELECT B.NOMBRECOMPLETO SOLICITANTE, C.NOMBRECOMPLETO RESPONSABLE,D.NOMBRECOMPLETO ALTERNO,G.NOMBRECOMPLETO INVOLUCRADO,
	E.NOMBRE TIPOREQUERIMIENTO, F.NOMBRE DESESTADO,F.DESCRIPCION COLORESTADO, H.NOMBRE AREA,CONVERT(VARCHAR,a.FECHAREGISTRO,103) FECREGISTRO,
	CONVERT(VARCHAR,a.FECHAINIPLAZO,103) FECINIPLAZO, CONVERT(VARCHAR,a.FECHAFINPLAZO,103) FECFINPLAZO ,  A.* 
	FROM SCI_TAREAS A WITH(NOLOCK)  
	LEFT JOIN SCI_PERSONA B WITH(NOLOCK) ON  B.IDPERSONA=A.ID_SOLICITANTE
	LEFT JOIN SCI_MAESTRODETALLE E WITH(NOLOCK) ON  E.CODIGO=A.TIPO AND E.IDMAESTRO=21
	LEFT JOIN SCI_MAESTRODETALLE F WITH(NOLOCK) ON  F.CODIGO=A.ESTADO AND F.IDMAESTRO=22
	LEFT JOIN SCI_PERSONA C WITH(NOLOCK) ON  C.IDPERSONA=A.ID_RESPONSABLE
	LEFT JOIN SCI_PERSONA D WITH(NOLOCK) ON  D.IDPERSONA=A.ID_ALTERNO
	LEFT JOIN SCI_PERSONA G WITH(NOLOCK) ON  G.IDPERSONA=A.ID_INVOLUCRADO
	LEFT JOIN SCI_MAESTRODETALLE H WITH(NOLOCK) ON  H.CODIGO=A.IDAREA AND H.IDMAESTRO=24





ALTER PROCEDURE [dbo].[SCI_ListarAlerta] 
@ID					int =null,
@IDTAREA		 	int =null,
@IDRESPONSABLE		int =null,
@ESTADO				int=null
AS
BEGIN

	SELECT B.NOMBRECOMPLETO SOLICITANTE, C.NOMBRECOMPLETO RESPONSABLE,C.EMAIL,
	E.NOMBRE TIPOREQUERIMIENTO, F.NOMBRE DESESTADO,F.DESCRIPCION COLORESTADO, H.NOMBRE AREA,CONVERT(VARCHAR,a.FECHAREGISTRO,103) FECREGISTRO,
	CONVERT(VARCHAR,a.FECHAINIPLAZO,103) FECINIPLAZO, CONVERT(VARCHAR,a.FECHAFINPLAZO,103) FECFINPLAZO ,  I.* 
	FROM SCI_ALERTAS  I WITH(NOLOCK)  
	INNER JOIN SCI_TAREAS A WITH(NOLOCK) ON  I.IDRELACIONADO=A.ID_TAREA  AND I.TIPOALERTA=1
	LEFT JOIN SCI_PERSONA B WITH(NOLOCK) ON  B.IDPERSONA=A.ID_SOLICITANTE
	LEFT JOIN SCI_MAESTRODETALLE E WITH(NOLOCK) ON  E.CODIGO=A.TIPO AND E.IDMAESTRO=21
	LEFT JOIN SCI_MAESTRODETALLE F WITH(NOLOCK) ON  F.CODIGO=A.ESTADO AND F.IDMAESTRO=22
	LEFT JOIN SCI_PERSONA C WITH(NOLOCK) ON  C.IDPERSONA=I.IDPERSONA
	LEFT JOIN SCI_MAESTRODETALLE H WITH(NOLOCK) ON  H.CODIGO=A.IDAREA AND H.IDMAESTRO=24
	WHERE  
		(@IDTAREA = 0	or @IDTAREA is null					or A.ID_TAREA = @IDTAREA )
	and (@ID = 0		or @ID is null						or I.ID=@ID)
	and (@IDRESPONSABLE = 0 or @IDRESPONSABLE is null		or I.IDPERSONA=@IDRESPONSABLE)
	and (@ESTADO = 0	or @ESTADO is null					or I.ESTADO=@ESTADO)
END


GO
CREATE PROCEDURE [dbo].[SCI_ListarTareaAsignadas] 
@IDTAREA		 	int =null,
@IDSOLICITANTE		int =null,
@IDRESPONSABLE		int =null,
@IDALTERNO			int =null,
@TIPO				int =null,
@IDINVOLUCRADO		int =null,
@IDAREA				int =null,
@NROREQUERIMIENTO	varchar(25)=null,
@TAREA				varchar(100)=null,
@ESTADO				int=null,
@FechaInicio		DATETIME =null,
@FechaFinal			DATETIME=null 
AS
BEGIN

	SELECT B.NOMBRECOMPLETO SOLICITANTE, C.NOMBRECOMPLETO RESPONSABLE,D.NOMBRECOMPLETO ALTERNO,G.NOMBRECOMPLETO INVOLUCRADO,
	E.NOMBRE TIPOREQUERIMIENTO, F.NOMBRE DESESTADO,F.DESCRIPCION COLORESTADO, H.NOMBRE AREA,CONVERT(VARCHAR,a.FECHAREGISTRO,103) FECREGISTRO,
	CONVERT(VARCHAR,a.FECHAINIPLAZO,103) FECINIPLAZO, CONVERT(VARCHAR,a.FECHAFINPLAZO,103) FECFINPLAZO ,  A.* 
	FROM SCI_TAREAS A WITH(NOLOCK)  
	INNER JOIN SCI_ALERTAS I WITH(NOLOCK) ON  I.IDRELACIONADO=A.ID_TAREA  AND I.TIPOALERTA=1
	LEFT JOIN SCI_PERSONA B WITH(NOLOCK) ON  B.IDPERSONA=A.ID_SOLICITANTE
	LEFT JOIN SCI_MAESTRODETALLE E WITH(NOLOCK) ON  E.CODIGO=A.TIPO AND E.IDMAESTRO=21
	LEFT JOIN SCI_MAESTRODETALLE F WITH(NOLOCK) ON  F.CODIGO=A.ESTADO AND F.IDMAESTRO=22
	LEFT JOIN SCI_PERSONA C WITH(NOLOCK) ON  C.IDPERSONA=A.ID_RESPONSABLE
	LEFT JOIN SCI_PERSONA D WITH(NOLOCK) ON  D.IDPERSONA=A.ID_ALTERNO
	LEFT JOIN SCI_PERSONA G WITH(NOLOCK) ON  G.IDPERSONA=A.ID_INVOLUCRADO
	LEFT JOIN SCI_MAESTRODETALLE H WITH(NOLOCK) ON  H.CODIGO=A.IDAREA AND H.IDMAESTRO=24
	WHERE  
		(@IDTAREA = 0	or @IDTAREA is null					or A.ID_TAREA = @IDTAREA )
	and (@TIPO = 0 or @TIPO is null							or A.TIPO=@TIPO)
	and (@IDRESPONSABLE = 0 or @IDRESPONSABLE is null		or I.IDPERSONA=@IDRESPONSABLE)
	and (@IDAREA = 0 or @IDAREA is null						or A.IDAREA=@IDAREA)
	and (@NROREQUERIMIENTO is null							or A.NROREQUERIMIENTO like '%'+@NROREQUERIMIENTO+'%')
	and (@TAREA is null										or A.TAREA like '%'+@TAREA+'%')
	and (@ESTADO = 0 or @ESTADO is null						or A.ESTADO=@ESTADO)
	AND	(@FechaInicio IS NULL OR  @FechaFinal IS NULL OR convert(date, A.FECHAREGISTRO, 103) between convert(date, @FechaInicio, 103) and convert(date, @FechaFinal, 103))

END

GO
ALTER PROCEDURE [dbo].[SCI_ListarTarea] 
@IDTAREA		 	int =null,
@IDSOLICITANTE		int =null,
@IDRESPONSABLE		int =null,
@IDALTERNO			int =null,
@TIPO				int =null,
@IDINVOLUCRADO		int =null,
@IDAREA				int =null,
@NROREQUERIMIENTO	varchar(25)=null,
@TAREA				varchar(100)=null,
@ESTADO				int=null,
@FechaInicio		DATETIME =null,
@FechaFinal			DATETIME=null 
AS
BEGIN

	SELECT B.NOMBRECOMPLETO SOLICITANTE, C.NOMBRECOMPLETO RESPONSABLE,D.NOMBRECOMPLETO ALTERNO,G.NOMBRECOMPLETO INVOLUCRADO,
	E.NOMBRE TIPOREQUERIMIENTO, F.NOMBRE DESESTADO,F.DESCRIPCION COLORESTADO, H.NOMBRE AREA,CONVERT(VARCHAR,a.FECHAREGISTRO,103) FECREGISTRO,
	CONVERT(VARCHAR,a.FECHAINIPLAZO,103) FECINIPLAZO, CONVERT(VARCHAR,a.FECHAFINPLAZO,103) FECFINPLAZO ,  A.* 
	FROM SCI_TAREAS A WITH(NOLOCK)  
	LEFT JOIN SCI_PERSONA B WITH(NOLOCK) ON  B.IDPERSONA=A.ID_SOLICITANTE
	LEFT JOIN SCI_MAESTRODETALLE E WITH(NOLOCK) ON  E.CODIGO=A.TIPO AND E.IDMAESTRO=21
	LEFT JOIN SCI_MAESTRODETALLE F WITH(NOLOCK) ON  F.CODIGO=A.ESTADO AND F.IDMAESTRO=22
	LEFT JOIN SCI_PERSONA C WITH(NOLOCK) ON  C.IDPERSONA=A.ID_RESPONSABLE
	LEFT JOIN SCI_PERSONA D WITH(NOLOCK) ON  D.IDPERSONA=A.ID_ALTERNO
	LEFT JOIN SCI_PERSONA G WITH(NOLOCK) ON  G.IDPERSONA=A.ID_INVOLUCRADO
	LEFT JOIN SCI_MAESTRODETALLE H WITH(NOLOCK) ON  H.CODIGO=A.IDAREA AND H.IDMAESTRO=24
	WHERE  
		(@IDTAREA = 0	or @IDTAREA is null					or A.ID_TAREA = @IDTAREA )
	and (@IDSOLICITANTE = 0 or @IDSOLICITANTE is null		or A.ID_SOLICITANTE=@IDSOLICITANTE)
	and (@TIPO = 0 or @TIPO is null							or A.TIPO=@TIPO)
	and (@IDRESPONSABLE = 0 or @IDRESPONSABLE is null		or A.ID_RESPONSABLE=@IDRESPONSABLE)
	and (@IDALTERNO = 0 or @IDALTERNO is null				or A.ID_ALTERNO=@IDALTERNO)
	and (@IDINVOLUCRADO = 0 or @IDINVOLUCRADO is null		or A.ID_INVOLUCRADO=@IDINVOLUCRADO)
	and (@IDAREA = 0 or @IDAREA is null						or A.IDAREA=@IDAREA)
	and (@NROREQUERIMIENTO is null							or A.NROREQUERIMIENTO like '%'+@NROREQUERIMIENTO+'%')
	and (@TAREA is null										or A.TAREA like '%'+@TAREA+'%')
	and (@ESTADO = 0 or @ESTADO is null						or A.ESTADO=@ESTADO)
	AND	(@FechaInicio IS NULL OR  @FechaFinal IS NULL OR convert(date, A.FECHAREGISTRO, 103) between convert(date, @FechaInicio, 103) and convert(date, @FechaFinal, 103))

END

--SELECT * FROM [SCI_PARAMETROS]

GO

ALTER PROCEDURE [dbo].[SCI_ListarAccion] 
@IDTAREA			int =null,
@IDACCION			int =null,
@IDRESPONSABLE		int =null,
@ESTADO				int =null
AS
BEGIN
SELECT B.NOMBRECOMPLETO RESPONSABLE,F.NOMBRE DESESTADO ,CONVERT(VARCHAR,a.FECHA,103) FECREGISTRO,
	F.DESCRIPCION COLORESTADO,A.* 
	FROM SCI_ACCIONES A WITH(NOLOCK)
	LEFT JOIN  SCI_PERSONA B		WITH(NOLOCK) ON  B.IDPERSONA=A.ID_RESPONSABLE
	INNER JOIN SCI_TAREAS C			WITH(NOLOCK) ON  C.ID_TAREA=A.ID_TAREA 
	LEFT JOIN  SCI_MAESTRODETALLE F WITH(NOLOCK) ON  F.CODIGO=A.ESTADO AND F.IDMAESTRO=22
WHERE 
		(@IDTAREA = 0 or @IDTAREA is null					or A.ID_TAREA=@IDTAREA)
	and (@IDACCION = 0 or @IDACCION is null					or A.ID_ACCION=@IDACCION)
	and	(@IDRESPONSABLE = 0 or @IDRESPONSABLE is null		or A.ID_RESPONSABLE=@IDRESPONSABLE)	
	and (@ESTADO = 0  or @ESTADO is null					or A.ESTADO=@ESTADO)
END
GO

GO

CREATE TABLE [dbo].[SCI_TICKET](
	[IDTICKET] [int] NOT NULL,
	[CODIGO] [varchar](25)  NULL,
	[TIPO] [int]  NULL,
	[DESCRIPCION] [varchar](100)  NULL,
	[FECHAREGISTRO] [datetime]  NULL,
	[IDSOLICITANTE] [int]  NULL,
	[IDRESPONSABLE] [int]  NULL,
	[FECHAINI] [datetime]  NULL,
	[FECHAFIN] [datetime]  NULL,
	[CANTIDAD] [int]  NULL,
	[OBSERVACION] [varchar](500)  NULL,
	[SOLUCION] [varchar](500)  NULL,
	[DOCUMENTACION] [varchar](300)  NULL,
	[ESTADO] [int]  NULL,
	[USUARIOCREACION] [varchar](25)  NULL,
	[USUARIODMODIFICACION] [varchar](25)  NULL,
	[FECHACREACION] [datetime]  NULL,
	[FECHAMODIFICACION] [datetime]  NULL,
	[IPMODIFICACION] [varchar](25)  NULL,
	[IPCREACION] [varchar](25)  NULL,
	PRIMARY KEY  CLUSTERED (IDTICKET ASC),
	FOREIGN KEY ([IDSOLICITANTE]) REFERENCES SCI_PERSONA(IDPERSONA),
	FOREIGN KEY ([IDRESPONSABLE]) REFERENCES SCI_PERSONA(IDPERSONA)
) ON [PRIMARY]

GO




CREATE TABLE SCI_ALERTAS
( 
	ID                   int  NOT NULL ,	
	IDRELACIONADO        int  NOT NULL ,
	TIPOALERTA           int  NULL ,
	PROCESO              int  NULL ,
	CODIGO               varchar(20)  NULL ,
	IDPERSONA            int  NULL ,
	ESTADO               int  NULL ,	
	USUARIOCREACION      varchar(20)  NULL ,
	FECHACREACION        datetime  NULL ,
	 PRIMARY KEY  CLUSTERED (IDRELACIONADO, ID ASC),
	 FOREIGN KEY (IDPERSONA) REFERENCES SCI_PERSONA(IDPERSONA)
)
go

CREATE TABLE SCI_TAREAS
( 
	ID_TAREA             int  NOT NULL ,
	NROREQUERIMIENTO     varchar(20)  NULL ,
	TIPO                 int  NULL ,
	TAREA                varchar(100)  NULL ,	
	ID_SOLICITANTE       int  NULL ,
	ID_RESPONSABLE       int  NULL ,
	ID_ALTERNO           int  NULL ,	
	ID_INVOLUCRADO       int  NULL ,
	IDAREA               int  NULL ,
	FECHAREGISTRO        datetime  NULL ,
	FECHAINIPLAZO        datetime  NULL ,
	FECHAFINPLAZO        datetime  NULL ,	
	PRECIO               money  NULL ,
	OBSERVACION          varchar(200)  NULL ,
	ESTADO               int  NULL ,
	USUARIOCREACION      varchar(20)  NULL ,
	USUARIOMODIFICACION  varchar(20)  NULL ,
	FECHACREACION        datetime  NULL ,
	FECHAMODIFICACION    datetime  NULL ,
	 PRIMARY KEY  CLUSTERED (ID_TAREA ASC),
	 FOREIGN KEY (ID_SOLICITANTE) REFERENCES SCI_PERSONA(IDPERSONA),
	 FOREIGN KEY (ID_RESPONSABLE) REFERENCES SCI_PERSONA(IDPERSONA),
	 FOREIGN KEY (ID_ALTERNO) REFERENCES SCI_PERSONA(IDPERSONA),
	 FOREIGN KEY (ID_INVOLUCRADO) REFERENCES SCI_PERSONA(IDPERSONA)
)
go


CREATE TABLE SCI_ACCIONES
( 
	ID_ACCION            int  NOT NULL ,	
	ID_TAREA             int  NOT NULL ,	
	ID_RESPONSABLE       int  NULL ,
	DESCRIPCION          varchar(200)  NULL ,
	FECHA                datetime  NULL ,	
	OBSERVACION          varchar(200)  NULL ,
	ESTADO               int  NULL ,
	USUARIOCREACION      varchar(20)  NULL ,
	USUARIOMODIFICACION  varchar(20)  NULL ,
	FECHACREACION        datetime  NULL ,
	FECHAMODIFICACION    datetime  NULL ,
	 PRIMARY KEY  CLUSTERED (ID_TAREA ASC,ID_ACCION ASC),
	 FOREIGN KEY (ID_TAREA) REFERENCES SCI_TAREAS(ID_TAREA)
)
go