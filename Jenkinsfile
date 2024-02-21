pipeline {
    agent any
    environment {
        DOCKER_REGISTRY = 'llanos1205'
    }
    stages {
        stage('Build') {
            steps {

                checkout scm
                
                stash 'source'
                
                sh 'dotnet restore'

                sh 'dotnet build SOFT806.WebApp --configuration Release --no-restore --verbosity minimal --property WarningLevel=0'

                sh 'dotnet test SOFT806.Tests --configuration Release --no-restore --verbosity minimal --logger "trx;LogFileName=unit_tests.xml"'
                
               mstest(
                    testResultsFile: 'SOFT806.Tests/TestResults/unit_tests.xml',
                    keepLongStdio: true,
                    failOnError: true
                )

            }
            }

        stage('Package') {
                    steps {
                        checkout scm
        
                        // Use appropriate plugin for Docker setup
                        sh 'docker buildx install'
        
                        withCredentials([usernamePassword(credentialsId: 'dockerhub-credentials', passwordVariable: 'DOCKER_TOKEN', usernameVariable: 'DOCKER_USERNAME')]) {
                            sh 'rm ~/.docker/config.json'
                            sh 'echo $DOCKER_TOKEN | docker login -u $DOCKER_USERNAME --password-stdin'
                        }
        
                        sh 'docker buildx build . --file ./Dockerfile --push --tag $DOCKER_REGISTRY/soft806-api:latest'
                        sh 'docker buildx build . --file ./Migrations.Dockerfile --push --tag $DOCKER_REGISTRY/soft806-migrations:latest'
                    }
                }
        
        stage('Deploy') {
      
            steps {
                checkout scm
                
                                withCredentials([usernamePassword(credentialsId: 'dockerhub-credentials', passwordVariable: 'DOCKER_TOKEN', usernameVariable: 'DOCKER_USERNAME')]) {
                                    sh 'rm ~/.docker/config.json'
                                   sh 'echo $DOCKER_TOKEN | docker login -u $DOCKER_USERNAME --password-stdin'
                                }
                
                                sh 'docker-compose stop'
                                sh 'docker-compose rm -f'
                                sh 'docker-compose pull'
                                sh 'docker-compose up -d'
            }
        }
    }
}
