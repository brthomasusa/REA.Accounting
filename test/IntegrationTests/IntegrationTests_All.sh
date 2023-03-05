#!/bin/sh

# Script to run integration test classes in series, each one alters the database so can't be run in paralell

 dotnet test --no-restore --nologo -v q --filter EmployeeAggregateEndPointTests
 echo "--------------------------> EmployeeAggregateEndPointTests completed <--------------------------"
echo -en '\n'

 dotnet test --no-restore --nologo -v q --filter HrCommandHandler_Tests
 echo "--------------------------> HrCommandHandler_Tests  completed <--------------------------"
echo -en '\n'
 
 dotnet test --no-restore --nologo -v q --filter HrQueryHandler_Tests
 echo "--------------------------> HrQueryHandler_Tests completed <--------------------------"
echo -en '\n'
 
 dotnet test --no-restore --nologo -v q --filter DbContextRetrieve_Tests
 echo "--------------------------> DbContextRetrieve_Tests completed <--------------------------"
echo -en '\n'
 
 dotnet test --no-restore --nologo -v q --filter PersonAndHrSpecification_Test
 echo "--------------------------> PersonAndHrSpecification_Test completed <--------------------------"
echo -en '\n'
 
 dotnet test --no-restore --nologo -v q --filter PersonHrCreate_Tests
 echo "--------------------------> PersonHrCreate_Tests completed <--------------------------"
echo -en '\n'

 dotnet test --no-restore --nologo -v q --filter EmployeeAggregateRepo_Tests 
 echo "--------------------------> EmployeeAggregateRepo_Tests completed <--------------------------"
echo -en '\n'

 dotnet test --no-restore --nologo -v q --filter CompanyAggregateRepo_Tests
 echo "--------------------------> CompanyAggregateRepo_Tests completed <--------------------------"
echo -en '\n'
