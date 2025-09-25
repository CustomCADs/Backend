# Application
resource "aws_elastic_beanstalk_application" "customcads_app" {
  name        = "CustomCADs"
  description = "The Application for CustomCADs"
}

# Key pair
resource "aws_key_pair" "customcads_key_pair" {
  key_name   = "customcads-key-pair"
  public_key = file("eb-keys/customcads.pub")
}
