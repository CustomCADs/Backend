# RDS instance
resource "aws_db_instance" "customcads_database" {
  identifier     = "customcads-staging-db"
  db_name        = "CustomCADs_Staging"
  instance_class = "db.t3.micro"

  engine         = "postgres"
  engine_version = "16.8"
  port           = 5432

  username = local.db_username
  password = local.db_password

  vpc_security_group_ids = [data.terraform_remote_state.common.outputs.db_security_group]
  db_subnet_group_name   = data.terraform_remote_state.common.outputs.db_subnet_group

  storage_type          = "gp2"
  allocated_storage     = 20
  max_allocated_storage = 1000
  storage_encrypted     = true

  multi_az                   = false
  auto_minor_version_upgrade = true
  maintenance_window         = "Sun:08:00-Sun:08:30"
  skip_final_snapshot        = true
  parameter_group_name       = "default.postgres16"
}
