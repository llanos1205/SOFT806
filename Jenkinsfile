pipeline {
    agent any

    stages {
        stage('Build') {
            steps {

                checkout scm
                
                stash 'source'
                
                sh 'dotnet restore'

                sh 'dotnet build SOFT806.WebApp --configuration Release --no-restore --verbosity minimal --property WarningLevel=0'

                sh 'dotnet test SOFT806.Tests --configuration Release --no-restore --verbosity minimal --logger "trx;LogFileName=test-results/unit_tests.xml"'
                cd 'SOFT806.Tests/TestResults/test-results'
                touch '*.xml'
                junit skipPublishingChecks: true, 
                      testResults: '**/test-results/*.xml',
                      allowEmptyResults: true

            }

         
        }
        stage('Deploy') {
      
            steps {
                unstash 'source'

                sh 'docker-compose -f docker-compose.yaml up -d'
            }
        }
    }
}
