locals {
  origin_id = "eb-customcads_app"
}

# Certificate Manager
resource "aws_acm_certificate" "customcads_certificate" {
  domain_name               = "customcads.com"
  subject_alternative_names = ["*.customcads.com", "*.api.customcads.com"]

  validation_method = "DNS"
  key_algorithm     = "EC_prime256v1"

  lifecycle {
    create_before_destroy = true
  }
}

# Cache policy
resource "aws_cloudfront_cache_policy" "customcads_cache_policy" {
  name        = "CustomCADsCachePolicy"
  comment     = "Cache policy for the CustomCADs API"
  default_ttl = 3600
  max_ttl     = 86400
  min_ttl     = 0

  parameters_in_cache_key_and_forwarded_to_origin {
    cookies_config {
      cookie_behavior = "all"
    }
    headers_config {
      header_behavior = "whitelist"
      headers {
        items = [
          "Origin",
          "Csrf-Token",
          "Idempotency-Key",
          "Stripe-Signature",
          "Access-Control-Request-Headers",
          "Access-Control-Request-Method"
        ]
      }
    }
    query_strings_config {
      query_string_behavior = "all"
    }
  }
}
