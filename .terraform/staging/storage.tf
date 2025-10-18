# User Files Bucket
resource "cloudflare_r2_bucket" "customcads_bucket" {
  account_id    = var.cloudflare_account_id
  name          = "customcads-staging-bucket"
  location      = "EEUR"
  storage_class = "Standard"
}
resource "cloudflare_r2_bucket_cors" "customcads_bucket_cors" {
  account_id  = var.cloudflare_account_id
  bucket_name = cloudflare_r2_bucket.customcads_bucket.name
  rules = [{
    allowed = {
      headers = ["*"]
      methods = ["HEAD", "GET", "PUT", "POST", "DELETE"]
      origins = ["https://main.d1apebjda2nmov.amplifyapp.com", "https://customcads.com", "https://www.customcads.com"]
    }
    expose_headers  = ["ETag", "x-amz-id-2", "x-amz-request-id", "x-amz-meta-file-name"]
    max_age_seconds = 3000
    id              = "CustomCADs Staging Bucket CORS rule"
  }]
}
