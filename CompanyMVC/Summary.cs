/*
 
1)ViewBag: Dynamic Properity [Based on Dynamic Keyword (not Key & Value)] => .NetFramwork 4
1.1)can Store it in Variable and not require any casting So it alawys be weakly type
Ex:String data= ViewBag.Message
1.2)it Slower than Viewdata cause Viewdata Strongly type 
2)Viewdata :KeyValue Pair[Based On Dictionary] =>.NetFramWork 3.5
2.1)can store value in Variable but require Casting from dictionary to the type of variable(strongly type)
EX: String VDmessage=@ViewData["Message"] as String
- if we define ViewBag After define ViewData it will Override on it with the value of view bag
- both transfare data from action to view (one way transfare)
- All used to transfare data from Action To View OR View To LayOut

3)Life Time of Object That Created From CLR 
-All App Running =>AddSingliton() : Generate one object At Heap all run time [per Application]
-Per Request Time => AddScopped() : Generate one object Per Request And after executing delete it
[per Request]
- AddTriansiant() =>
[Per Operations] (many Operations takeplace to execute one request) /Create one object to execute specific operation in the request

 */
