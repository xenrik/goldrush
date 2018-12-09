using UnityEngine;
using UnityEditor;

/**
 * Used to indicate a token supports 'closing'
 */
public interface CloseableToken {
    char CloseTokenSymbol { get; }

    /** Tell the token that the close symbol was found during parsing */
    void ClosedParsing();

    /** Tell the token that the close resolve has been reached */
    void ClosedResolve();
}