using UnityEngine;
using UnityEditor;

/**
 * Used to indicate a token supports 'closing'
 */
public interface CloseableToken : Token {
    /** Mark this token as 'Closed' */
    void Close();
}