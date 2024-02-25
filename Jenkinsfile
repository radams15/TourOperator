pipeline {
    agent any
    
    stages {
        /*stage('SAST') {
            environment {
                SEMGREP_APP_TOKEN = credentials('semgrep')
            }
            agent {
                docker {
                    image 'docker.io/returntocorp/semgrep:latest'
                }
            }
            steps {
                script {
                    sh 'semgrep ci'
                }
            }
        }*/

        stage('SCA') {
            environment {
                API_KEY = credentials('nvd')
            }
            steps {
                dependencyCheck additionalArguments: '-o ./ -s ./ --prettyPrint -f ALL --nvdApiKey: ${API_KEY}', odcInstallation: 'OWASP Dependency-Check'
                dependencyCheckPublisher pattern: 'dependency-check-report.xml'
            }
        }
    
        stage('Build') {
            agent {
                docker {
                    image 'mcr.microsoft.com/dotnet/sdk:7.0'
                }
            }
            steps {
                sh 'dotnet restore'
                sh 'dotnet publish -c Release -o out'
                sh 'echo "#!/bin/sh\ndocker run -p5000:80 -v .:/App -w /App -it --rm mcr.microsoft.com/dotnet/aspnet:7.0 ./TourOperator" > out/run.sh && chmod +x ./out/run.sh'
                
                sh 'ls'
                
                script {
                    archiveArtifacts artifacts: 'out/'
                }
            }
        }
    }
}
