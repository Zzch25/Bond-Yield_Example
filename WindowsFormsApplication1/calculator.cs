/*
 *Zachary Job
 *calculator.cs
 *2/1/17
 *
 *A wrapper for math functions



Increase accuracy after reading up on notes


 */
 
using System;

namespace fiyield
{
	internal class calculator : shared 
	{
		/*
		 *A placeholder for increasing function of the
		 *available math functions
		 */
		internal calculator()
		{
		}

		/*
		 *Calculate the price using of a fixed income asset
		 *using the integral regarding the involved vairables
		 *
		 *@PARAM: Coupon, the incentive rate for anual collection towards collection
		 *@PARAM: The total years
		 *@PARAM: The face value
		 *@PARAM: The yield
		 *@RETURN: The calculated price
		 */
		internal double CalcPrice(double coupon, int years, double face, double rate)
		{
			double
				CalcPrice_result;

			//Guess I'm rusty on integrals. Just going to keep that for a laugh
			//result = 0.5 * Math.Pow(coupon * face,2) * Math.Pow((rate + 1),-1 * years) + face / Math.Pow((rate + 1),years);

			CalcPrice_result = (coupon * face) * ((1.0 - (1.0 / Math.Pow(1.0 + rate, years))) / rate) + face * (1.0 / Math.Pow(1.0 + rate,(Double) years));

			return CalcPrice_result;
		}

		/*
		 *Calculate the yield using of a fixed income asset
		 *using the integral regarding the involved vairables
		 *
		 *I was a fan of Numerical Methods! This is merely a geometric series,
		 *guess and improve until the answer tightens. There is no real need
		 *to time this. Other than creating a tailored guessing algorithm
		 *this is fairly straight forward...
         *
         * Sorry for the innaccuracy I'm a tad rusty, but I'll look through my notes
         * in post
		 *
		 *@PARAM: Coupon, the incentive rate for anual collection towards collection
		 *@PARAM: The total years
		 *@PARAM: The face value
		 *@PARAM: The base price
		 *@RETURN: The calculated price
		 */
		internal double CalcYield(double coupon, int years, double face, double price)
		{
			double
                CalcYield_not_zero,
				CalcYield_Iterator_0,
				CalcYield_Iterator_1,
				CalcYield_total_resample,
				CalcYield_precision,
				CalcYield_coupon_value,
				CalcYield_resample,
				CalcYield_result;

			CalcYield_Iterator_1 = shared_sampling_precision;
			CalcYield_precision = -1;
			CalcYield_total_resample = 0;
			CalcYield_coupon_value = coupon * face;
			CalcYield_result = 1.0 / (1.0 + coupon); //A super simple guess - needs improving, and i need a review
			CalcYield_resample = CalcYield_result;
            CalcYield_not_zero = -1;

			while(CalcYield_Iterator_1-- > 0 && CalcYield_not_zero != 0)
			{
                CalcYield_not_zero = (price - face * Math.Pow(CalcYield_result, (double)years));

                for (CalcYield_Iterator_0 = 1;
                    CalcYield_Iterator_0 < years 
                        && CalcYield_precision != CalcYield_result
                        && CalcYield_not_zero != 0;
                    ++CalcYield_Iterator_0)
				{
					CalcYield_result = 
						1.0
						- CalcYield_coupon_value 
						* CalcYield_result
						* (1.0 - Math.Pow(CalcYield_result, (double)years))
						/ (price - face * Math.Pow(CalcYield_result, (double)years));
					CalcYield_precision = Math.Round(CalcYield_result);
					CalcYield_resample += CalcYield_result;

                    CalcYield_not_zero = (price - face * Math.Pow(CalcYield_result, (double)years));
                }

				CalcYield_precision = -1;
				CalcYield_total_resample += CalcYield_Iterator_0;
				CalcYield_result = CalcYield_resample / ((Double)CalcYield_total_resample);

                if (CalcYield_not_zero == 0)
                    CalcYield_result = 2;
            }

            if (CalcYield_not_zero != 0)
            {
                CalcYield_not_zero = (price - face * Math.Pow(CalcYield_result, (double)years));

                for (CalcYield_Iterator_0 = 1;
                    CalcYield_Iterator_0 < years
                        && CalcYield_precision != CalcYield_result
                        && CalcYield_not_zero != 0;
                    ++CalcYield_Iterator_0)
                {
                    CalcYield_result =
                        1.0
                        - CalcYield_coupon_value
                        * CalcYield_result
                        * (1.0 - Math.Pow(CalcYield_result, (double)years))
                        / (price - face * Math.Pow(CalcYield_result, (double)years));
                    CalcYield_precision = Math.Round(CalcYield_result);

                    CalcYield_not_zero = (price - face * Math.Pow(CalcYield_result, (double)years));
                }

                if (CalcYield_not_zero == 0)
                    CalcYield_result = 2;
            }

			return (1.0 - CalcYield_result) / CalcYield_result;
		}
	}
}
