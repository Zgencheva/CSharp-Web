Short explanation:

1. Bool variable naming - "hasLogging" instead of "logging";
2. I think that DateTime.UtcNow() should be applied,because it stores a date (as fas as I understand the task) for lalter calculations.
3. We use "_" mostly for private fields in classes;
4. If possible, can we receive the InterfaceManager from outside, because there are too many dependencies: IfaceManager, db...?;
5. this._db.GetLocaDB(dbCod) in variable - localDb; Code reusable.
6. Manager.Instance - if this makes an instance of Manager, it should also come from outside.
7. rabbitEvent?.parentId - it will pass null if rabbitEvent is null;
8. There is one try-catch that has nothing to catch.
9. Use LINQ where possible - the part with the if-else.
10. Better naming - use "details" instead of just "d";
11. string combinedLog - this one is never used.
12. details[0] is not correct, because it`s in the foreach.
13. StoreExceptionLogAsync - name it Async in order to be clear that there is asynchronous operations going on.
14. totalErrors++; occur in every try-catch.
15. String interpolation - I prefer using "$", not string.Format.