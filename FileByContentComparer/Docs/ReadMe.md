#Full trottle

This simple library uses buffered file reading for binary content comparision.

# Usage example
```csharp

 var comparer = new FileByContentComparer();
 var sameFile = comparer.Compare("FileA", "FileB");

```
