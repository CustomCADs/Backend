# Container Registry
resource "aws_ecr_repository" "customcads_container_registry" {
  name = "ninjatabg/customcads"
  encryption_configuration {
    encryption_type = "KMS"
  }
}

# User Files Bucket for Development
resource "aws_s3_bucket" "customcads_development_bucket" {
  bucket = "customcads-development-bucket"
}
resource "aws_s3_bucket_cors_configuration" "customcads_development_bucket_cors" {
  bucket = aws_s3_bucket.customcads_development_bucket.id

  cors_rule {
    allowed_headers = ["*"]
    allowed_methods = ["HEAD", "GET", "PUT", "POST", "DELETE"]
    allowed_origins = ["https://localhost:7295", "https://localhost:5173", "https://localhost:5174", "http://localhost:4173"]
    expose_headers  = ["ETag", "x-amz-id-2", "x-amz-request-id", "x-amz-meta-file-name"]
    max_age_seconds = 3000
  }
}

# Versions Bucket
resource "aws_s3_bucket" "customcads_versions" {
  bucket = "customcads-versions"
}

# Terraform Bucket
resource "aws_s3_bucket" "customcads_terraform" {
  bucket = "customcads-terraform-common"
}
resource "aws_s3_object" "customcads_terraform_state" {
  bucket = aws_s3_bucket.customcads_terraform.bucket
  key    = "terraform.tfstate"
  source = "terraform.tfstate"

}
