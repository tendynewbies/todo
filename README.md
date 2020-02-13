# ToDo
Nagarro Xamarin Assignment - ToDo

Problem Statement
Create a mobile app to fetch todos from a restful api and show on screen. Below are the detailed requirements.

Requirements
Screen	Use case	Details
Todo list screen	User can see todo list	1.	Screen has the title ‘Todo List’
2.	Screen shows todo list data fetched from network
3.	While getting data, show activity indicator
4.	In case of network unavailability or error, show error message and reload button
5.	Clicking on reload button, retry fetching data from network

API Specification 
•	Protocol HTTPS 
•	Hostname jsonplaceholder.typicode.com 
•	Method GET 
•	Endpoint /todos 
•	Response example: HTTP code 200 
[ 
  { 
    "userId": 1, 
    "id": 1, 
    "title": "delectus aut autem", 
    "completed": false 
  }, 
  { 
    "userId": 2, 
    "id": 2, 
    "title": "quis ut nam facilis et officia qui", 
    "completed": false 
  },
  … 
] 
