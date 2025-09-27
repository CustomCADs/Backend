# User Files Bucket
resource "aws_s3_bucket" "customcads_bucket" {
  bucket = "customcads-staging-bucket"
}
resource "aws_s3_bucket_cors_configuration" "customcads_bucket_cors" {
  bucket = aws_s3_bucket.customcads_bucket.id

  cors_rule {
    allowed_headers = ["*"]
    allowed_methods = ["HEAD", "GET", "PUT", "POST", "DELETE"]
    allowed_origins = ["https://main.d1apebjda2nmov.amplifyapp.com", "https://customcads.com", "https://www.customcads.com"]
    expose_headers  = ["ETag", "x-amz-id-2", "x-amz-request-id", "x-amz-meta-file-name"]
    max_age_seconds = 3000
  }
}
