using System;
using DI.Interfaces;

namespace DI.Models {
  public class Example : 
    IExampleScoped, IExampleSingleton, IExampleTransient, IExampleSingletonInstance 
  {
    public Guid ExampleId { get; set; }
    public Example() {
      ExampleId = Guid.NewGuid();
    }  
    public Example(Guid exampleId) {
      ExampleId = exampleId;
    }
  }
}