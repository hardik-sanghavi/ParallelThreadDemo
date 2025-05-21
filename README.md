This Demo covers the 2 API calls in a single thread and a parallel thread with the response time.
- SingleThread API, which has 2 API calls in sync manner and returns the  result, which takes more time than ParrallelThread
- ParallelThread API, which has 2 API calls asynchronously and returns combined result when both threads work is done

For Parallel Thread, we can use below code
' Task<string> task1 = Task.Run(() => RetriveDataFromMockAPI1());
 Task<string> task2 = Task.Run(() => RetriveDataFromMockAPI2());

 Task.WaitAll(task1, task2);
 '
