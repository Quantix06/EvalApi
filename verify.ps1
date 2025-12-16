$baseUrl = "http://localhost:5000/api"

function Test-Endpoint {
    param (
        [string]$Method,
        [string]$Url,
        [string]$Body = $null,
        [int]$ExpectedStatus = 200
    )

    Write-Host "Testing $Method $Url..." -NoNewline
    try {
        $params = @{
            Method = $Method
            Uri = "$baseUrl$Url"
            ErrorAction = "Stop"
        }
        if ($Body) {
            $params.Body = $Body
            $params.ContentType = "application/json"
        }

        $response = Invoke-RestMethod @params
        
        # If we are here, status is 2xx
        # We can't easily get the exact status code without -StatusCodeVariable in older PS, 
        # but if it didn't throw, it's likely 200 or 201 or 204.
        
        Write-Host " PASS" -ForegroundColor Green
        return $response

    } catch {
        $ex = $_.Exception
        $status = 0
        if ($ex.Response) {
            $status = [int]$ex.Response.StatusCode
        }

        if ($status -eq $ExpectedStatus) {
            Write-Host " PASS ($status)" -ForegroundColor Green
            return $null # No body usually for errors, or we can read stream
        } else {
            Write-Host " FAIL (Expected $ExpectedStatus, got $status)" -ForegroundColor Red
            Write-Host "Exception: $($ex.Message)"
        }
    }
    return $null
}

# 1. Health
Test-Endpoint -Method GET -Url "/health" -ExpectedStatus 200

# 2. Create User
$user = @{
    name = "John Doe"
    username = "johndoe"
    email = "john@example.com"
} | ConvertTo-Json

$createdUser = Test-Endpoint -Method POST -Url "/users" -Body $user -ExpectedStatus 201
if ($createdUser) {
    Write-Host "User Created: $($createdUser.id)"
}

# 3. Get Users
Test-Endpoint -Method GET -Url "/users" -ExpectedStatus 200

# 4. Create Post
if ($createdUser) {
    $userId = $createdUser.id
    $post = @{
        userId = $userId
        title = "My First Post"
        body = "This is the body of my first post."
    } | ConvertTo-Json

    $createdPost = Test-Endpoint -Method POST -Url "/users/$userId/posts" -Body $post -ExpectedStatus 201
    if ($createdPost) {
        Write-Host "Post Created: $($createdPost.id)"
    }

    # 5. Get Posts by User
    Test-Endpoint -Method GET -Url "/users/$userId/posts" -ExpectedStatus 200

    # 6. Get Post
    if ($createdPost) {
        $postId = $createdPost.id
        Test-Endpoint -Method GET -Url "/posts/$postId" -ExpectedStatus 200

        # 7. Update Post
        $updatePost = @{
            id = $postId
            userId = $userId
            title = "Updated Title"
            body = "Updated Body"
        } | ConvertTo-Json
        Test-Endpoint -Method PUT -Url "/posts/$postId" -Body $updatePost -ExpectedStatus 200

        # 8. Delete Post
        Test-Endpoint -Method DELETE -Url "/posts/$postId" -ExpectedStatus 204
        
        # Verify Delete
        Test-Endpoint -Method GET -Url "/posts/$postId" -ExpectedStatus 404
    }
}
