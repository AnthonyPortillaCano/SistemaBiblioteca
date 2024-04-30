export default function authHeader() {
  const userAuth = localStorage.getItem('userAuth');
  if (userAuth) {
    return { Authorization: 'Bearer ' + userAuth };
  } else {
    return {};
  }
}
