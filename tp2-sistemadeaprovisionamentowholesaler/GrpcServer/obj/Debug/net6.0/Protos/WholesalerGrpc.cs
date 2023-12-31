// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: Protos/wholesaler.proto
// </auto-generated>
#pragma warning disable 0414, 1591
#region Designer generated code

using grpc = global::Grpc.Core;

namespace GrpcServer {
  public static partial class wholesaler
  {
    static readonly string __ServiceName = "wholesaler";

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static void __Helper_SerializeMessage(global::Google.Protobuf.IMessage message, grpc::SerializationContext context)
    {
      #if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
      if (message is global::Google.Protobuf.IBufferMessage)
      {
        context.SetPayloadLength(message.CalculateSize());
        global::Google.Protobuf.MessageExtensions.WriteTo(message, context.GetBufferWriter());
        context.Complete();
        return;
      }
      #endif
      context.Complete(global::Google.Protobuf.MessageExtensions.ToByteArray(message));
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static class __Helper_MessageCache<T>
    {
      public static readonly bool IsBufferMessage = global::System.Reflection.IntrospectionExtensions.GetTypeInfo(typeof(global::Google.Protobuf.IBufferMessage)).IsAssignableFrom(typeof(T));
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static T __Helper_DeserializeMessage<T>(grpc::DeserializationContext context, global::Google.Protobuf.MessageParser<T> parser) where T : global::Google.Protobuf.IMessage<T>
    {
      #if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
      if (__Helper_MessageCache<T>.IsBufferMessage)
      {
        return parser.ParseFrom(context.PayloadAsReadOnlySequence());
      }
      #endif
      return parser.ParseFrom(context.PayloadAsNewBuffer());
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::GrpcServer.DomicilioWholesaler> __Marshaller_DomicilioWholesaler = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::GrpcServer.DomicilioWholesaler.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::GrpcServer.ReserveResult> __Marshaller_ReserveResult = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::GrpcServer.ReserveResult.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::GrpcServer.ActivateResult> __Marshaller_ActivateResult = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::GrpcServer.ActivateResult.Parser));

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::GrpcServer.DomicilioWholesaler, global::GrpcServer.ReserveResult> __Method_Reserve = new grpc::Method<global::GrpcServer.DomicilioWholesaler, global::GrpcServer.ReserveResult>(
        grpc::MethodType.Unary,
        __ServiceName,
        "Reserve",
        __Marshaller_DomicilioWholesaler,
        __Marshaller_ReserveResult);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::GrpcServer.DomicilioWholesaler, global::GrpcServer.ActivateResult> __Method_Activate = new grpc::Method<global::GrpcServer.DomicilioWholesaler, global::GrpcServer.ActivateResult>(
        grpc::MethodType.Unary,
        __ServiceName,
        "Activate",
        __Marshaller_DomicilioWholesaler,
        __Marshaller_ActivateResult);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::GrpcServer.DomicilioWholesaler, global::GrpcServer.ActivateResult> __Method_Deactivate = new grpc::Method<global::GrpcServer.DomicilioWholesaler, global::GrpcServer.ActivateResult>(
        grpc::MethodType.Unary,
        __ServiceName,
        "Deactivate",
        __Marshaller_DomicilioWholesaler,
        __Marshaller_ActivateResult);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::GrpcServer.DomicilioWholesaler, global::GrpcServer.ActivateResult> __Method_Terminate = new grpc::Method<global::GrpcServer.DomicilioWholesaler, global::GrpcServer.ActivateResult>(
        grpc::MethodType.Unary,
        __ServiceName,
        "Terminate",
        __Marshaller_DomicilioWholesaler,
        __Marshaller_ActivateResult);

    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::GrpcServer.WholesalerReflection.Descriptor.Services[0]; }
    }

    /// <summary>Base class for server-side implementations of wholesaler</summary>
    [grpc::BindServiceMethod(typeof(wholesaler), "BindService")]
    public abstract partial class wholesalerBase
    {
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task<global::GrpcServer.ReserveResult> Reserve(global::GrpcServer.DomicilioWholesaler request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task<global::GrpcServer.ActivateResult> Activate(global::GrpcServer.DomicilioWholesaler request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task<global::GrpcServer.ActivateResult> Deactivate(global::GrpcServer.DomicilioWholesaler request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task<global::GrpcServer.ActivateResult> Terminate(global::GrpcServer.DomicilioWholesaler request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

    }

    /// <summary>Creates service definition that can be registered with a server</summary>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public static grpc::ServerServiceDefinition BindService(wholesalerBase serviceImpl)
    {
      return grpc::ServerServiceDefinition.CreateBuilder()
          .AddMethod(__Method_Reserve, serviceImpl.Reserve)
          .AddMethod(__Method_Activate, serviceImpl.Activate)
          .AddMethod(__Method_Deactivate, serviceImpl.Deactivate)
          .AddMethod(__Method_Terminate, serviceImpl.Terminate).Build();
    }

    /// <summary>Register service method with a service binder with or without implementation. Useful when customizing the  service binding logic.
    /// Note: this method is part of an experimental API that can change or be removed without any prior notice.</summary>
    /// <param name="serviceBinder">Service methods will be bound by calling <c>AddMethod</c> on this object.</param>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public static void BindService(grpc::ServiceBinderBase serviceBinder, wholesalerBase serviceImpl)
    {
      serviceBinder.AddMethod(__Method_Reserve, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::GrpcServer.DomicilioWholesaler, global::GrpcServer.ReserveResult>(serviceImpl.Reserve));
      serviceBinder.AddMethod(__Method_Activate, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::GrpcServer.DomicilioWholesaler, global::GrpcServer.ActivateResult>(serviceImpl.Activate));
      serviceBinder.AddMethod(__Method_Deactivate, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::GrpcServer.DomicilioWholesaler, global::GrpcServer.ActivateResult>(serviceImpl.Deactivate));
      serviceBinder.AddMethod(__Method_Terminate, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::GrpcServer.DomicilioWholesaler, global::GrpcServer.ActivateResult>(serviceImpl.Terminate));
    }

  }
}
#endregion
