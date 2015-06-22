
-- Update Plan
update Subscription set Name = '$30 per year for 1 pet, 1 MRA', Description = '' where IsPromotionCode = 0 and PromotionCode = 'HSBC' and IsTrial = 0

-- Update promo Code
update Subscription set Name = N'Free 40 days trial for 1 pet with 1 MRA', Description = '', Amount = 0 where IsPromotionCode = 1 and PromotionCode = 'HSBC' and IsTrial = 1