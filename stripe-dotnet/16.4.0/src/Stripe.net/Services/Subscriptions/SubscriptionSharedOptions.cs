﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Stripe.Infrastructure;

namespace Stripe
{
    public abstract class SubscriptionSharedOptions : StripeBaseOptions, ISupportMetadata
    {
        /// <summary>
        /// A non-negative decimal between 0 and 100, with at most two decimal places. This represents the percentage of the subscription invoice subtotal that will be transferred to the application owner’s Stripe account. The request must be made with an OAuth key in order to set an application fee percentage. For more information, see the application fees <see href="https://stripe.com/docs/connect/subscriptions#collecting-fees-on-subscriptions">documentation</see>.
        /// </summary>
        [JsonProperty("application_fee_percent")]
        public decimal? ApplicationFeePercent { get; set; }

        /// <summary>
        /// One of <see cref="StripeBilling" />. When charging automatically, Stripe will attempt to pay this subscription at the end of the cycle using the default source attached to the customer. When sending an invoice, Stripe will email your customer an invoice with payment instructions. Defaults to <c>charge_automatically</c>.
        /// </summary>
        [JsonProperty("billing")]
        public StripeBilling? Billing { get; set; }

        /// <summary>
        /// The code of the coupon to apply to this subscription. A coupon applied to a subscription will only affect invoices created for that particular subscription.
        /// </summary>
        [JsonProperty("coupon")]
        public string CouponId { get; set; }

        /// <summary>
        /// Number of days a customer has to pay invoices generated by this subscription. Only valid for subscriptions where <c>billing=send_invoice</c>.
        /// </summary>
        [JsonProperty("days_until_due")]
        public int? DaysUntilDue { get; set; }

        /// <summary>
        /// A set of key/value pairs that you can attach to a subscription object. It can be useful for storing additional information about the subscription in a structured format.
        /// </summary>
        [JsonProperty("metadata")]
        public Dictionary<string, string> Metadata { get; set; }

        /// <summary>
        /// Boolean (default <c>true</c>). Use with a <c>billing_cycle_anchor</c> timestamp to determine whether the customer will be invoiced a prorated amount until the anchor date. If <c>false</c>, the anchor period will be free (similar to a trial).
        /// </summary>
        [JsonProperty("prorate")]
        public bool? Prorate { get; set; }

        /// <summary>
        /// ID of a Token or a Source, as returned by <see cref="https://stripe.com/docs/elements">Elements</see>. You must provide a source if the customer does not already have a valid source attached, and you are subscribing the customer to be charged automatically for a plan that is not free. Passing <c>source</c> will create a new source object, make it the customer default source, and delete the old customer default if one exists. If you want to add an additional source, instead use the <see cref="https://stripe.com/docs/api#create_card">card creation API</see> to add the card and then the <see cref="https://stripe.com/docs/api#update_customer">customer update API</see> to set it as the default. Whenever you attach a card to a customer, Stripe will automatically validate the card.
        /// <para>If you use Source, you cannot use SourceCard also.</para>
        /// </summary>
        [JsonProperty("source")]
        public string Source { get; set; }

        /// <summary>
        /// <see cref="SourceCard"/> instance containing a user’s credit card details. You must provide a source if the customer does not already have a valid source attached, and you are subscribing the customer to be charged automatically for a plan that is not free. Passing <c>source</c> will create a new source object, make it the customer default source, and delete the old customer default if one exists. If you want to add an additional source, instead use the <see cref="https://stripe.com/docs/api#create_card">card creation API</see> to add the card and then the <see cref="https://stripe.com/docs/api#update_customer">customer update API</see> to set it as the default. Whenever you attach a card to a customer, Stripe will automatically validate the card.
        /// <para>If you use SourceCard, you cannot use Source also.</para>
        /// </summary>
        [JsonProperty("source")]
        public SourceCard SourceCard { get; set; }

        /// <summary>
        /// A non-negative decimal (with at most four decimal places) between 0 and 100. This represents the percentage of the subscription invoice subtotal that will be calculated and added as tax to the final amount each billing period. For example, a plan which charges $10/month with a <c>tax_percent</c> of 20.0 will charge $12 per invoice.
        /// </summary>
        [JsonProperty("tax_percent")]
        public decimal? TaxPercent { get; set; }

        #region TrialEnd
        /// <summary>
        /// Date representing the end of the trial period the customer will get before being charged for the first time. Set <see cref="EndTrialNow"/> to <c>true</c> to end the customer’s trial immediately.
        /// </summary>
        public DateTime? TrialEnd { get; set; }
        public bool EndTrialNow { get; set; }

        [JsonProperty("trial_end")]
        internal string TrialEndInternal => EndTrialNow ? "now" : TrialEnd?.ConvertDateTimeToEpoch().ToString();
        #endregion

        /// <summary>
        /// Boolean. Decide whether to use the default trial on the plan when creating a subscription.
        /// </summary>
        [JsonProperty("trial_from_plan")]
        public bool? TrialFromPlan { get; set; }

        [Obsolete("Use Source or SourceCard")]
        [JsonProperty("card")]
        public StripeCreditCardOptions Card { get; set; }

        [Obsolete("Use Items")]
        [JsonProperty("plan")]
        public string PlanId { get; set; }

        [Obsolete("Use Items")]
        [JsonProperty("quantity")]
        public int? Quantity { get; set; }
    }
}