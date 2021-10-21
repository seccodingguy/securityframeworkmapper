# Security Framework Mapper Application

<div><p>The mapper application is an attempt to bring together the NIST Cybersecurity Framework with the NIST 800, NIST Risk Management Framework, FedRAMP, and DoD STIGS in a singular pane of glass. The goal is to use the mapping to drive automation in DevSecOps by using the STIGS as the guide to script the control and then automatically include the script where required within the DevSecOps pipelines.</p></div>

<p>This is a work-in-progress and will be expanded to include ISO, Cloud Security Matrix, and MITRE Att&ck Framework.</p>

<div>
<p>The application is written in C# using Visual Studio 2019 built using the .NET Framework v4.7.</p>
<p>MySQL is the database with an expected schema of frameworks (if you change the schema, be sure to update the SQL statements in the backup file). A frameworks_backup.sql file is included that has all of the data for the frameworks that have been parsed and mapped to date.</p>
</div>

<div>
  <p>The database connection settings are saved in the app.config file. A connection string already exists in the app.config with placeholders of what information requires updating. <b>NOTE</b>: I plan to provide an example C# application that will pull the connection string and authentication details for Hashicorp Vault. It is stongly encouraged to use a similar method to protect sensitive database connection information.</p>
</div>

<div><p>Public use is granted under the MIT License</p></div>
