#Supported Endpoints

### Week
The following endpoints all support both GET and PUT requests.  Any JSON body recieved from a GET request is a valid body 
to transmit in a PUT request.

```week/current/{user}```
Gets the current week, based on PST.

```week/previous/{user}```
Gets the previous week, based on PST.

```week/{date}/{user}```
Gets a specific week by date, e.g. week/2015-9-1/stefango

```week/{year}/{week}/{user}```
Gets a specific week by week number, e.g. week/2015/32/stefango

Example Body:
```
{
  "year": 2015,
  "week": 35,
  "weekEndDate": "2015-09-05T00:00:00Z",
  "alias": "stefango",
  "entries": [
    {
      "projectName": "Animal Farm",
      "timeAllocation": 40,
      "health": 1,
      "notes": "A fairy story"
    },
    {
      "projectName": "Coming Up For Air",
      "timeAllocation": 30,
      "health": 2,
      "notes": ""
    }
  ],
  "PartitionKey": "2015_35",
  "RowKey": "stefango",
  "Timestamp": "2015-10-07T18:48:03.3964306+00:00",
  "ETag": "W/\"datetime'2015-10-07T18%3A48%3A03.3964306Z'\""
}
```

### Projects
Projects are added automatically if a week entry containing a new project is submitted.  You can query them via the API.

```projects```

or

```projects?contains={some text}```

Example JSON Result

```[
  "Animal Farm",
  "Coming Up For Air"
]```