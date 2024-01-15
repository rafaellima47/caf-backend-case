# Face Comparison

This project consists of two separate APIs for face comparison: the Anonymization Service built with Flask, and the Comparison Service built with ASP.NET.

## Steps

### Clone the project repository using the following command:

```bash
git clone git@github.com:rafaellima47/face-comparison.git
```

### Navigate to the project directory and start the Docker Compose project using the following command:

```bash
sudo docker-compose up --build
```

---

## Endpoints

### 1. Anonymize

- **URL:** `FaceComparison/anonymize`
- **Method:** `POST`
- **Form Data:**
  - `identifier`: A unique GUID for the face.
  - `image`: The image file containing the face to be anonymized.

Anonymizes the face in the input image, stores it in the database, and returns the anonymized face object.

**Example Request:**

```http
POST /anonymize HTTP/1.1
Content-Type: multipart/form-data; boundary=your_boundary_string
```

**Example Response:**

```json
{
  "Identifier": "3f2504e0-4f89-11d3-9a0c-0305e82c3301",
  "Embedding": [0.1, 0.2, ...]
}
```

## 2. Compare

- **URL:** `FaceComparison/compare`
- **Method:** `POST`
- **Form Data:**
  - `identifier`: A unique GUID for the face.
  - `image`: The image file containing the face to be anonymized.
  
Compares the input face with the stored face identified by the provided identifier. Returns the comparison result.

**Example Request:**

```http
POST /compare HTTP/1.1
Content-Type: multipart/form-data; boundary=your_boundary_string
```

**Example Response:**

```json
{
  "IsMatch": true,
  "Distance": 0.32
}
```
