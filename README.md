# TriangleServerAndMVC

## Triangle Server

This is the SOAP rest service which computes the calculations necessary for determinine the type of triangle given by its three lengths. It also handles other computations like checking for errors, calculating the current page as well as how many pages there are in the database, where each record is a page.

Accessed at http://localhost:53479/TriangleTool.asmx

## Triangle MVC

This is a quick tool which allows the user to manually edit the results of a triangle. There really are no restrictions on what can be added here so users can get really creative with the results they wish to display.

Accessed at http://localhost:51253/Triangles

## Instructions for how to develop, use, and test the code.

#### Generating the SQL Database

Using the script.sql file, build an SQL Server with the database name TriangleDB under the server name ".".

#### Configurating your IP Address

Find your local IP Address by typing `ipconfig` into the terminal of your choice, and replace all IP addresses starting with 192.168.*.* with your own IP Address. The reason for the specific IP address is because I am currently developing an Android Application in Java which sends and captures SOAP requests and responses in order to calculate and determine the nature of the triangles provided by their lengths.
