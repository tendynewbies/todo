# ToDo

# Problem Statement
A Xamarin Foms(Android & iOS) mobile app to fetch todos from a restful api and display.
<br/>
<br/>
Below are the detailed requirements.

# Requirements
| Screen        | Use case           | Details  |
| ------------- |:-------------:| -----:|
| Todo list screen      | User can see todo list | 1.	Screen has the title ‘Todo List’<br/>2.	Screen shows todo list data fetched from network<br/>3.	While getting data, show activity indicator<br/>4.	In case of network unavailability or error, show error message and reload button<br/>5.	Clicking on reload button, retry fetching data from network



# API Specification 
•	Protocol HTTPS<br/>
•	Hostname jsonplaceholder.typicode.com<br/>
•	Method GET<br/>
•	Endpoint /todos<br/> 
•	Response example: HTTP code 200<br/>

[<br/> 
  { <br/>
    "userId": 1, <br/>
    "id": 1, <br/>
    "title": "delectus aut autem", <br/>
    "completed": false <br/>
  }, <br/>
  { <br/>
    "userId": 2, <br/>
    "id": 2, <br/>
    "title": "quis ut nam facilis et officia qui", <br/>
    "completed": false <br/>
  },<br/>
  … <br/>
] <br/><br/><br/>
