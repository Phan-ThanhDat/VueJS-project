# Homework Assignment

Thank you for your interest in applying to a developer position at Aibidia.

## Scenario

> We at Aibidia just launched our recent solution, Careers application, which allows users to submit CV's to us in a very clear and fun way.
> Unfortunately marketing already launched the solution, so it's your task to make some "key features" to it.

The code feels quite similar to our current technology stack, so it is also a chance for you to get familiar in our structure.

## Assignment F: (only for applicants in frontend positions)

Your homework is to create a new VueRouter endpoint ```/hm/user/resume``` and implement your (fictional or actual) resume (CV) as you see fit.

We pay attention to following things:

1. How well you adjust your coding style to match ours.
2. What components you create and how they are created.
3. We look at the quality of your comments and typescripted components.
4. Looks are secondary to good DOM structure and clean CSS code.
5. Write at least one test case, so that all test cases pass.
6. If you can't complete the task, that is fine. In this case document your work, progress and how you would continue.

Average time of completion of this homework should not exceed 1h.

## Assignment B: (only for applications in backend positions)

Front-end developer is trying to use endpoint `HTTP GET /api/resumes/` to get list of resumes from API. This does not work currently, as mapping to Microsoft SQL Database is broken.

**Task:** Your task is to fix the API endpoint, and returns a proper set of resumes stored in SQL database.

You may want to setup local SQLServer to host database with appropriate Resumes-table, make some dummy data for demonstration purposes.

We pay attention to following things:

1. How you choose to fix the issue
2. What classes you create and how they are created.
3. We look at the quality of your comments.
4. Looks are secondary to good DOM structure and clean CSS code.
5. SQL script to insert data into SQLServer.
6. If you can't fix the error, that is fine. In this case document your troubleshooting and where do you think the error is.

Average time of completion of this homework should not exceed 1h.

## How to install & run

Actually we trust you to be familiar already with our toolset.

## Things you might want to know

### Front

`Front\personal-config.js` personal config for the API directory:

Use either with localhost server

``` javascript
module.exports = {
    apiUrl:'"http://localhost:49413/api"',
};
```

or set to empty string to mockup `/api/resumes` API responses.

``` javascript
module.exports = {
    apiUrl:'""',
};
```

- Default UI port is 8080.
- `Front\src\store\modules\resume-store.ts` contains VueX store
- `Front\src\lib\api\endpoints\hm\createResumeEndpoint.ts` contains Resume endpoint calling.
- `Front\src\types\Resume.ts` contains Resume Typescript typings.

### Back

- Default API port is 49413.
- `Back\Homework\Controller\ResumesController.cs` API controller for the resumes endpoint.
- `Back\Homework\DTOs\ResumeDto.cs` Resume DTO that front expects.
- `Back\Aibidia.API.Common\Models\Resume.cs` Resume DBModel.

## Other

Please include feedback on the code structure / style. What you would do differently and why, as we are constantly willing to evolve, and expect the same from you.

## Returning of the assignment

Return the assignment code with your changes in ZIP folder in the same email thread in which you received the code from the recruiter.

**Please make sure you won't zip or send any NuGet / NPM packages.**
