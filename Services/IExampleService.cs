using System;

namespace DI.Interfaces {
  public interface IExampleService {
    Guid ExampleId { get; }      
  }
  public interface IExampleTransient : IExampleService {
  }
  public interface IExampleScoped : IExampleService {
  }
  public interface IExampleSingleton : IExampleService {
  }
  public interface IExampleSingletonInstance : IExampleService {
  }
}