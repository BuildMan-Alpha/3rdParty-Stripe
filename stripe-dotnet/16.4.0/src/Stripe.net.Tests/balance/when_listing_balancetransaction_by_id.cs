﻿using Machine.Specifications;

namespace Stripe.Tests
{
    public class when_listing_balancetransactions_by_id
    {
        private static StripeBalanceTransaction _stripeBalanceTransaction;
        private static StripeList<StripeBalanceTransaction> _stripeBalanceTransactions;
        private static StripeBalanceService _stripeBalanceService;
        private static StripeCharge _stripeCharge;

        Establish context = () =>
        {
            var _stripeChargeService = new StripeChargeService();
            _stripeBalanceService = new StripeBalanceService();

            _stripeCharge = _stripeChargeService.Create(test_data.stripe_charge_create_options.ValidCard());

            var _stripeBalanceTransactionListOptions = new StripeBalanceTransactionListOptions
            {
                SourceId = _stripeCharge.Id,
                Type = "charge"
            };

            _stripeBalanceTransactions = _stripeBalanceService.List(_stripeBalanceTransactionListOptions);
        };

        Because of = () =>
            _stripeBalanceTransaction = _stripeBalanceService.Get(_stripeBalanceTransactions.Data[0].Id);

        It should_have_the_right_amount = () =>
            _stripeBalanceTransaction.Amount.ShouldEqual(_stripeCharge.Amount);

        It should_have_the_right_id = () =>
            _stripeBalanceTransaction.SourceId.ShouldEqual(_stripeCharge.Id);
    }
}
