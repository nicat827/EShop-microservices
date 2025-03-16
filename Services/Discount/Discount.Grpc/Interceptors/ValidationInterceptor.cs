﻿using FluentValidation;
using Grpc.Core.Interceptors;
using Grpc.Core;
using FluentValidation.Results;

namespace Discount.Grpc.Interceptors
{
    public class ValidationInterceptor : Interceptor
    {
        private readonly IServiceProvider _serviceProvider;

        public ValidationInterceptor(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
            TRequest request,
            ServerCallContext context,
            UnaryServerMethod<TRequest, TResponse> continuation)
        {
            var validator = _serviceProvider.GetService<IValidator<TRequest>>();

            if (validator != null)
            {
                ValidationResult validationResult = await validator.ValidateAsync(request);

                if (!validationResult.IsValid)
                {
                    var errors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                    throw new RpcException(new Status(StatusCode.InvalidArgument, $"Validation failed: {errors}"));
                }
            }

            return await continuation(request, context);
        }
    }
}
