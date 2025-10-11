variable "region" {
  type        = string
  nullable    = false
  description = "Specify the AWS Region"
}

variable "cloudflare_account_id" {
  type        = string
  nullable    = false
  description = "Specify the Cloudflare Account ID"
}

variable "cloudflare_api_token" {
  type        = string
  nullable    = false
  description = "Specify the Cloudflare Account token value"
}
