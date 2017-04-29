package Generated.Protobuf;

import static io.grpc.stub.ClientCalls.asyncUnaryCall;
import static io.grpc.stub.ClientCalls.asyncServerStreamingCall;
import static io.grpc.stub.ClientCalls.asyncClientStreamingCall;
import static io.grpc.stub.ClientCalls.asyncBidiStreamingCall;
import static io.grpc.stub.ClientCalls.blockingUnaryCall;
import static io.grpc.stub.ClientCalls.blockingServerStreamingCall;
import static io.grpc.stub.ClientCalls.futureUnaryCall;
import static io.grpc.MethodDescriptor.generateFullMethodName;
import static io.grpc.stub.ServerCalls.asyncUnaryCall;
import static io.grpc.stub.ServerCalls.asyncServerStreamingCall;
import static io.grpc.stub.ServerCalls.asyncClientStreamingCall;
import static io.grpc.stub.ServerCalls.asyncBidiStreamingCall;
import static io.grpc.stub.ServerCalls.asyncUnimplementedUnaryCall;
import static io.grpc.stub.ServerCalls.asyncUnimplementedStreamingCall;

/**
 * <pre>
 * ------------- SERVICE -------------
 * </pre>
 */
@javax.annotation.Generated(
    value = "by gRPC proto compiler (version 1.2.0)",
    comments = "Source: AppService.proto")
public final class AppServiceGrpc {

  private AppServiceGrpc() {}

  public static final String SERVICE_NAME = "AppService";

  // Static method descriptors that strictly reflect the proto.
  @io.grpc.ExperimentalApi("https://github.com/grpc/grpc-java/issues/1901")
  public static final io.grpc.MethodDescriptor<Generated.Protobuf.Request,
      Generated.Protobuf.Response> METHOD_SEND_REQUEST =
      io.grpc.MethodDescriptor.create(
          io.grpc.MethodDescriptor.MethodType.BIDI_STREAMING,
          generateFullMethodName(
              "AppService", "sendRequest"),
          io.grpc.protobuf.ProtoUtils.marshaller(Generated.Protobuf.Request.getDefaultInstance()),
          io.grpc.protobuf.ProtoUtils.marshaller(Generated.Protobuf.Response.getDefaultInstance()));

  /**
   * Creates a new async stub that supports all call types for the service
   */
  public static AppServiceStub newStub(io.grpc.Channel channel) {
    return new AppServiceStub(channel);
  }

  /**
   * Creates a new blocking-style stub that supports unary and streaming output calls on the service
   */
  public static AppServiceBlockingStub newBlockingStub(
      io.grpc.Channel channel) {
    return new AppServiceBlockingStub(channel);
  }

  /**
   * Creates a new ListenableFuture-style stub that supports unary and streaming output calls on the service
   */
  public static AppServiceFutureStub newFutureStub(
      io.grpc.Channel channel) {
    return new AppServiceFutureStub(channel);
  }

  /**
   * <pre>
   * ------------- SERVICE -------------
   * </pre>
   */
  public static abstract class AppServiceImplBase implements io.grpc.BindableService {

    /**
     */
    public io.grpc.stub.StreamObserver<Generated.Protobuf.Request> sendRequest(
        io.grpc.stub.StreamObserver<Generated.Protobuf.Response> responseObserver) {
      return asyncUnimplementedStreamingCall(METHOD_SEND_REQUEST, responseObserver);
    }

    @java.lang.Override public final io.grpc.ServerServiceDefinition bindService() {
      return io.grpc.ServerServiceDefinition.builder(getServiceDescriptor())
          .addMethod(
            METHOD_SEND_REQUEST,
            asyncBidiStreamingCall(
              new MethodHandlers<
                Generated.Protobuf.Request,
                Generated.Protobuf.Response>(
                  this, METHODID_SEND_REQUEST)))
          .build();
    }
  }

  /**
   * <pre>
   * ------------- SERVICE -------------
   * </pre>
   */
  public static final class AppServiceStub extends io.grpc.stub.AbstractStub<AppServiceStub> {
    private AppServiceStub(io.grpc.Channel channel) {
      super(channel);
    }

    private AppServiceStub(io.grpc.Channel channel,
        io.grpc.CallOptions callOptions) {
      super(channel, callOptions);
    }

    @java.lang.Override
    protected AppServiceStub build(io.grpc.Channel channel,
        io.grpc.CallOptions callOptions) {
      return new AppServiceStub(channel, callOptions);
    }

    /**
     */
    public io.grpc.stub.StreamObserver<Generated.Protobuf.Request> sendRequest(
        io.grpc.stub.StreamObserver<Generated.Protobuf.Response> responseObserver) {
      return asyncBidiStreamingCall(
          getChannel().newCall(METHOD_SEND_REQUEST, getCallOptions()), responseObserver);
    }
  }

  /**
   * <pre>
   * ------------- SERVICE -------------
   * </pre>
   */
  public static final class AppServiceBlockingStub extends io.grpc.stub.AbstractStub<AppServiceBlockingStub> {
    private AppServiceBlockingStub(io.grpc.Channel channel) {
      super(channel);
    }

    private AppServiceBlockingStub(io.grpc.Channel channel,
        io.grpc.CallOptions callOptions) {
      super(channel, callOptions);
    }

    @java.lang.Override
    protected AppServiceBlockingStub build(io.grpc.Channel channel,
        io.grpc.CallOptions callOptions) {
      return new AppServiceBlockingStub(channel, callOptions);
    }
  }

  /**
   * <pre>
   * ------------- SERVICE -------------
   * </pre>
   */
  public static final class AppServiceFutureStub extends io.grpc.stub.AbstractStub<AppServiceFutureStub> {
    private AppServiceFutureStub(io.grpc.Channel channel) {
      super(channel);
    }

    private AppServiceFutureStub(io.grpc.Channel channel,
        io.grpc.CallOptions callOptions) {
      super(channel, callOptions);
    }

    @java.lang.Override
    protected AppServiceFutureStub build(io.grpc.Channel channel,
        io.grpc.CallOptions callOptions) {
      return new AppServiceFutureStub(channel, callOptions);
    }
  }

  private static final int METHODID_SEND_REQUEST = 0;

  private static final class MethodHandlers<Req, Resp> implements
      io.grpc.stub.ServerCalls.UnaryMethod<Req, Resp>,
      io.grpc.stub.ServerCalls.ServerStreamingMethod<Req, Resp>,
      io.grpc.stub.ServerCalls.ClientStreamingMethod<Req, Resp>,
      io.grpc.stub.ServerCalls.BidiStreamingMethod<Req, Resp> {
    private final AppServiceImplBase serviceImpl;
    private final int methodId;

    MethodHandlers(AppServiceImplBase serviceImpl, int methodId) {
      this.serviceImpl = serviceImpl;
      this.methodId = methodId;
    }

    @java.lang.Override
    @java.lang.SuppressWarnings("unchecked")
    public void invoke(Req request, io.grpc.stub.StreamObserver<Resp> responseObserver) {
      switch (methodId) {
        default:
          throw new AssertionError();
      }
    }

    @java.lang.Override
    @java.lang.SuppressWarnings("unchecked")
    public io.grpc.stub.StreamObserver<Req> invoke(
        io.grpc.stub.StreamObserver<Resp> responseObserver) {
      switch (methodId) {
        case METHODID_SEND_REQUEST:
          return (io.grpc.stub.StreamObserver<Req>) serviceImpl.sendRequest(
              (io.grpc.stub.StreamObserver<Generated.Protobuf.Response>) responseObserver);
        default:
          throw new AssertionError();
      }
    }
  }

  private static final class AppServiceDescriptorSupplier implements io.grpc.protobuf.ProtoFileDescriptorSupplier {
    @java.lang.Override
    public com.google.protobuf.Descriptors.FileDescriptor getFileDescriptor() {
      return Generated.Protobuf.AppServiceOuterClass.getDescriptor();
    }
  }

  private static volatile io.grpc.ServiceDescriptor serviceDescriptor;

  public static io.grpc.ServiceDescriptor getServiceDescriptor() {
    io.grpc.ServiceDescriptor result = serviceDescriptor;
    if (result == null) {
      synchronized (AppServiceGrpc.class) {
        result = serviceDescriptor;
        if (result == null) {
          serviceDescriptor = result = io.grpc.ServiceDescriptor.newBuilder(SERVICE_NAME)
              .setSchemaDescriptor(new AppServiceDescriptorSupplier())
              .addMethod(METHOD_SEND_REQUEST)
              .build();
        }
      }
    }
    return result;
  }
}
