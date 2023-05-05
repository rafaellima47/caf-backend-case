from flask import Flask, request, jsonify
import base64
import cv2
import numpy as np
from deepface import DeepFace

app = Flask(__name__)

def get_embeddings(image):
    try:
        embedding = DeepFace.represent(image, model_name='Facenet', enforce_detection=False)
        return embedding
    except Exception as e:
        print(f"Error in get_embeddings: {e}")
        return None

@app.route('/anonymize', methods=['POST'])
def anonymize():
    if 'image' not in request.files:
        return jsonify({'error': 'No image provided.'}), 400

    file = request.files['image']
    file_bytes = np.asarray(bytearray(file.read()), dtype=np.uint8)
    image = cv2.imdecode(file_bytes, cv2.IMREAD_COLOR)

    embeddings = get_embeddings(image)

    if embeddings is None:
        return jsonify({'error': 'Could not generate embeddings.'}), 500

    return jsonify(embeddings)

if __name__ == '__main__':
    app.run(host='0.0.0.0', port=5000, debug=True)