﻿using Machine.Specifications;

namespace Stripe.Tests
{
    public class when_creating_a_charge_with_an_api_key
    {
        protected static StripeChargeCreateOptions StripeChargeCreateOptions;
        protected static StripeCharge StripeCharge;
        protected static StripeCard StripeCard;

        private static StripeChargeService _stripeChargeService;

        Establish context = () =>
        {
            _stripeChargeService = new StripeChargeService();
            StripeChargeCreateOptions = test_data.stripe_charge_create_options.ValidCard();
        };

        Because of = () =>
        {
            StripeCharge = _stripeChargeService.Create(StripeChargeCreateOptions);
            StripeCard = StripeCharge.Source.Card;
        };

#pragma warning disable 169, 414
        Behaves_like<charge_behaviors> behaviors;
#pragma warning restore 169, 414

    }
}