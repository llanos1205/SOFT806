pipeline {
    agent any

    stages {
        stage('Build') {
            steps {
                // Get some code from a GitHub repository
                git url: 'https://github.com/llanos1205/SOFT806.git',
                    branch: 'main'

                  // Restore dependencies
                sh 'dotnet restore'

                // Build application
                sh 'dotnet build SOFT703A2.WebApp --configuration Release --no-restore'

                // Run tests and collect coverage
                sh 'dotnet test SOFT806.Tests --configuration Release --no-restore'

            }

         
        }
        stage('Deploy') {
      
            steps {
                // Get the code
                git url: 'https://github.com/llanos1205/SOFT806.git',
                    branch: 'main'

                // Deploy logic with docker-compose (assuming Docker installed)
                sh 'docker-compose -f docker-compose.yaml up -d'
            }
        }
    }
}
