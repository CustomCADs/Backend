terraform {
  required_providers {
    aws = {
      source  = "hashicorp/aws"
      version = "~> 5.0"
    }
  }
}

provider "aws" {
  region = var.region
}

data "terraform_remote_state" "common" {
  backend = "s3"

  config = {
    bucket = "customcads-terraform-common"
    key    = "terraform.tfstate"
    region = var.region
  }
}
