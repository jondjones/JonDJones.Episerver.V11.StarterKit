GO
PRINT N'Dropping [HangFire].[FK_HangFire_State_Job]...';


GO
ALTER TABLE [HangFire].[State] DROP CONSTRAINT [FK_HangFire_State_Job];


GO
PRINT N'Dropping [HangFire].[FK_HangFire_JobParameter_Job]...';


GO
ALTER TABLE [HangFire].[JobParameter] DROP CONSTRAINT [FK_HangFire_JobParameter_Job];


GO
PRINT N'Dropping [HangFire].[Schema]...';


GO
DROP TABLE [HangFire].[Schema];


GO
PRINT N'Dropping [HangFire].[Job]...';


GO
DROP TABLE [HangFire].[Job];


GO
PRINT N'Dropping [HangFire].[State]...';


GO
DROP TABLE [HangFire].[State];


GO
PRINT N'Dropping [HangFire].[JobParameter]...';


GO
DROP TABLE [HangFire].[JobParameter];


GO
PRINT N'Dropping [HangFire].[JobQueue]...';


GO
DROP TABLE [HangFire].[JobQueue];


GO
PRINT N'Dropping [HangFire].[Server]...';


GO
DROP TABLE [HangFire].[Server];


GO
PRINT N'Dropping [HangFire].[List]...';


GO
DROP TABLE [HangFire].[List];


GO
PRINT N'Dropping [HangFire].[Set]...';


GO
DROP TABLE [HangFire].[Set];


GO
PRINT N'Dropping [HangFire].[Counter]...';


GO
DROP TABLE [HangFire].[Counter];


GO
PRINT N'Dropping [HangFire].[Hash]...';


GO
DROP TABLE [HangFire].[Hash];


GO
PRINT N'Dropping [HangFire].[AggregatedCounter]...';


GO
DROP TABLE [HangFire].[AggregatedCounter];


GO
PRINT N'Dropping [HangFire]...';


GO
DROP SCHEMA [HangFire];


GO
PRINT N'Update complete.';


GO