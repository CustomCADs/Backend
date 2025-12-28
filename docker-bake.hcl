# Groups
group "default" {
    targets = ["dev"]
}

# Variables
variable "dockerhub_repository" {
    default = "docker.io/customcads/backend"
}

variable "awsecr_repostory" {
    default = "222634368027.dkr.ecr.us-east-1.amazonaws.com/ninjatabg/customcads"
}

# Dev
target "dev" {
    context = "."
    dockerfile = "Dockerfile"
    tags = ["customcads:dev"]
}

target "dockerhub_dev" {
    inherits = ["dev"]
    tags = ["${dockerhub_repository}:dev"]
}

target "awsecr_dev" {
    inherits = ["dev"]
    tags = ["${awsecr_repostory}:dev"]
}

# Staging
target "staging" {
    context = "."
    dockerfile = "Dockerfile.staging"
    tags = ["customcads:staging"]
}

target "dockerhub_staging" {
    inherits = ["staging"]
    tags = ["${dockerhub_repository}:staging"]
}

target "awsecr_staging" {
    inherits = ["staging"]
    tags = ["${awsecr_repostory}:staging"]
}

# Printing
target "production" {
    context = "."
    dockerfile = "Dockerfile.production"
    tags = ["customcads:production"]
}

target "dockerhub_production" {
    inherits = ["production"]
    tags = ["${dockerhub_repository}:production"]
}

target "awsecr_production" {
    inherits = ["production"]
    tags = ["${awsecr_repostory}:production"]
}
