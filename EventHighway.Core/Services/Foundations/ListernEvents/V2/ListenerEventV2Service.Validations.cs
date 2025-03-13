// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.ListenerEvents.V2;
using EventHighway.Core.Models.Services.Foundations.ListenerEvents.V2.Exceptions;

namespace EventHighway.Core.Services.Foundations.ListernEvents.V2
{
    internal partial class ListenerEventV2Service
    {
        private async ValueTask ValidateListenerEventV2OnAddAsync(ListenerEventV2 listenerEventV2)
        {
            ValidateListenerEventV2IsNotNull(listenerEventV2);

            Validate(
                (Rule: IsInvalid(listenerEventV2.Id),
                Parameter: nameof(ListenerEventV2.Id)),

                (Rule: IsInvalid(listenerEventV2.Response),
                Parameter: nameof(ListenerEventV2.Response)),

                (Rule: IsInvalid(listenerEventV2.EventId),
                Parameter: nameof(ListenerEventV2.EventId)),

                (Rule: IsInvalid(listenerEventV2.EventAddressId),
                Parameter: nameof(ListenerEventV2.EventAddressId)),

                (Rule: IsInvalid(listenerEventV2.EventListenerId),
                Parameter: nameof(ListenerEventV2.EventListenerId)),

                (Rule: IsInvalid(listenerEventV2.Status),
                Parameter: nameof(ListenerEventV2.Status)),

                (Rule: IsInvalid(listenerEventV2.CreatedDate),
                Parameter: nameof(ListenerEventV2.CreatedDate)),

                (Rule: IsInvalid(listenerEventV2.UpdatedDate),
                Parameter: nameof(ListenerEventV2.UpdatedDate)),

                (Rule: IsNotSameAs(
                    firstDate: listenerEventV2.CreatedDate,
                    secondDate: listenerEventV2.UpdatedDate,
                    secondDateName: nameof(ListenerEventV2.UpdatedDate)),

                Parameter: nameof(ListenerEventV2.CreatedDate)),

                (Rule: await IsNotRecentAsync(listenerEventV2.CreatedDate),
                Parameter: nameof(ListenerEventV2.CreatedDate)));
        }

        private async ValueTask ValidateListenerEventV2OnModifyAsync(ListenerEventV2 listenerEventV2)
        {
            ValidateListenerEventV2IsNotNull(listenerEventV2);

            Validate(
                (Rule: IsInvalid(listenerEventV2.Id),
                Parameter: nameof(ListenerEventV2.Id)),

                (Rule: IsInvalid(listenerEventV2.Response),
                Parameter: nameof(ListenerEventV2.Response)),

                (Rule: IsInvalid(listenerEventV2.EventId),
                Parameter: nameof(ListenerEventV2.EventId)),

                (Rule: IsInvalid(listenerEventV2.EventAddressId),
                Parameter: nameof(ListenerEventV2.EventAddressId)),

                (Rule: IsInvalid(listenerEventV2.EventListenerId),
                Parameter: nameof(ListenerEventV2.EventListenerId)),

                (Rule: IsInvalid(listenerEventV2.Status),
                Parameter: nameof(ListenerEventV2.Status)),

                (Rule: IsInvalid(listenerEventV2.CreatedDate),
                Parameter: nameof(ListenerEventV2.CreatedDate)),

                (Rule: IsInvalid(listenerEventV2.UpdatedDate),
                Parameter: nameof(ListenerEventV2.UpdatedDate)),

                (Rule: IsSameAs(
                    firstDate: listenerEventV2.UpdatedDate,
                    secondDate: listenerEventV2.CreatedDate,
                    secondDateName: nameof(ListenerEventV2.CreatedDate)),

                Parameter: nameof(ListenerEventV2.UpdatedDate)),

                (Rule: await IsNotRecentAsync(listenerEventV2.UpdatedDate),
                Parameter: nameof(ListenerEventV2.UpdatedDate)));
        }

        private static void ValidateListenerEventV2AgainstStorage(
            ListenerEventV2 incomingListenerEventV2,
            ListenerEventV2 storageListenerEventV2)
        {
            ValidateListenerEventV2Exists(
                listenerEventV2: storageListenerEventV2,
                listenerEventV2Id: incomingListenerEventV2.Id);

            Validate(
                (Rule: IsNotSameAsStorage(
                    firstDate: incomingListenerEventV2.CreatedDate,
                    secondDate: storageListenerEventV2.CreatedDate),

                Parameter: nameof(ListenerEventV2.CreatedDate)),

                (Rule: IsEarlierThan(
                    firstDate: incomingListenerEventV2.UpdatedDate,
                    secondDate: storageListenerEventV2.UpdatedDate),

                Parameter: nameof(ListenerEventV2.UpdatedDate)));
        }

        private static void ValidateListenerEventV2Id(Guid listenerEventV2Id)
        {
            Validate(
                (Rule: IsInvalid(listenerEventV2Id),
                Parameter: nameof(ListenerEventV2.Id)));
        }

        private static void ValidateListenerEventV2Exists(ListenerEventV2 listenerEventV2, Guid listenerEventV2Id)
        {
            if (listenerEventV2 is null)
            {
                throw new NotFoundListenerEventV2Exception(
                    message: $"Could not find listener event with id: {listenerEventV2Id}.");
            }
        }

        private static void ValidateListenerEventV2IsNotNull(ListenerEventV2 listenerEventV2)
        {
            if (listenerEventV2 is null)
            {
                throw new NullListenerEventV2Exception(
                    message: "Listener event is null.");
            }
        }

        private static dynamic IsInvalid(Guid id) => new
        {
            Condition = id == default,
            Message = "Required"
        };

        private static dynamic IsInvalid(string text) => new
        {
            Condition = string.IsNullOrWhiteSpace(text),
            Message = "Required"
        };

        private static dynamic IsInvalid(DateTimeOffset date) => new
        {
            Condition = date == default,
            Message = "Required"
        };

        private static dynamic IsInvalid<T>(T value) => new
        {
            Condition = IsInvalidEnum(value) is true,
            Message = "Value is not recognized"
        };

        private static dynamic IsNotSameAs(
            DateTimeOffset firstDate,
            DateTimeOffset secondDate,
            string secondDateName) => new
            {
                Condition = firstDate != secondDate,
                Message = $"Date is not the same as {secondDateName}"
            };

        private static dynamic IsSameAs(
            DateTimeOffset firstDate,
            DateTimeOffset secondDate,
            string secondDateName) => new
            {
                Condition = firstDate == secondDate,
                Message = $"Date is the same as {secondDateName}"
            };

        private static dynamic IsNotSameAsStorage(
            DateTimeOffset firstDate,
            DateTimeOffset secondDate) => new
            {
                Condition = firstDate != secondDate,
                Message = $"Date is not the same as storage"
            };

        private static dynamic IsEarlierThan(
            DateTimeOffset firstDate,
            DateTimeOffset secondDate) => new
            {
                Condition = firstDate < secondDate,
                Message = $"Date is earlier than storage"
            };

        private async ValueTask<dynamic> IsNotRecentAsync(DateTimeOffset date) => new
        {
            Condition = await IsDateNotRecentAsync(date),
            Message = "Date is not recent"
        };

        private async ValueTask<bool> IsDateNotRecentAsync(DateTimeOffset date)
        {
            DateTimeOffset currentDateTime =
                await this.dateTimeBroker.GetDateTimeOffsetAsync();

            TimeSpan timeDifference = currentDateTime.Subtract(value: date);

            return timeDifference.TotalSeconds is > 60 or < 0;
        }

        private static bool IsInvalidEnum<T>(T enumValue)
        {
            bool isDefined = Enum.IsDefined(
                enumType: typeof(T),
                value: enumValue);

            return isDefined is false;
        }

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidListenerEventV2Exception =
                new InvalidListenerEventV2Exception(
                    message: "Listener event is invalid, fix the errors and try again.");

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidListenerEventV2Exception.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidListenerEventV2Exception.ThrowIfContainsErrors();
        }
    }
}
