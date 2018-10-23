
DROP PROC IF EXISTS Course_AddCourse
GO
CREATE PROCEDURE Course_AddCourse
@code varchar(20),
@name varchar(60),
@credits decimal(5,2),
@numberOfStudents int
AS
BEGIN
INSERT INTO Course 
VALUES (
@code,
@name,
@credits,
@numberOfStudents)
END

--

DROP PROC IF EXISTS Course_DeleteCourse
GO
CREATE PROCEDURE Course_DeleteCourse
@code varchar(20)
AS
BEGIN
DELETE FROM Course
WHERE 
code = @code
END

--

DROP PROC IF EXISTS Course_GetCourse
GO
CREATE PROCEDURE Course_GetCourse
@code varchar(20)
AS
BEGIN
SELECT * FROM Course
WHERE (code = @code)
END

--

DROP PROC IF EXISTS Course_GetALLCourses
GO
CREATE PROCEDURE Course_GetAllCourses
AS
BEGIN
SELECT * FROM Course
END



