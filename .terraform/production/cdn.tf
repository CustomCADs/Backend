locals {
  origin_id = "eb-customcads_app"
}

# Production CloudFront Distribution
resource "aws_cloudfront_distribution" "customcads_cdn" {
  origin {
    domain_name = aws_elastic_beanstalk_environment.customcads_env.cname
    origin_id   = local.origin_id
    origin_shield {
      enabled              = true
      origin_shield_region = var.region
    }

    custom_origin_config {
      http_port              = 80
      https_port             = 443
      origin_protocol_policy = "http-only"
      origin_ssl_protocols   = ["TLSv1.2"]
    }
  }

  enabled         = true
  is_ipv6_enabled = true
  aliases         = ["api.customcads.com"]

  default_cache_behavior {
    allowed_methods = ["DELETE", "GET", "HEAD", "OPTIONS", "PATCH", "POST", "PUT"]
    cached_methods  = ["GET", "HEAD"]

    target_origin_id       = local.origin_id
    cache_policy_id        = data.terraform_remote_state.common.outputs.cloudfront_cache_policy_id
    viewer_protocol_policy = "redirect-to-https"
  }

  price_class = "PriceClass_100"
  restrictions {
    geo_restriction {
      restriction_type = "whitelist"
      locations        = ["US", "CA", "GB", "DE", "BG", "RO"]
    }
  }

  viewer_certificate {
    cloudfront_default_certificate = false
    acm_certificate_arn            = data.terraform_remote_state.common.outputs.certificate_arn
    ssl_support_method             = "sni-only"
    minimum_protocol_version       = "TLSv1.2_2018"
  }
}
